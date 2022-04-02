#include "TestPdb.h"
#include "..\src\PDB_Util.h"

template <class A, class B>
static array<B>^ ToManagedArrayCast(const A* arr, uint32_t size)
{
	array<B>^ managed_arr = gcnew array<B>(size);
	for (uint32_t i = 0; i < size; i++)
	{
		managed_arr[i] = (B)arr[i];
	}
	return managed_arr;
}

template <class T>
static array<T>^ ToManagedArray(const T* arr, uint32_t size)
{
	return ToManagedArrayCast<T, T>(arr, size);
}

RawPdbNet::Pdb::Pdb(array<unsigned char>^ data)
{
	pin_ptr<Byte> p = &data[0];
	_pdb = new PDB::RawFile(p);
}

RawPdbNet::Pdb::~Pdb()
{
	delete _pdb;
}

uint32_t RawPdbNet::Pdb::GetStreamDirectoryNumBlocks()
{
	return _pdb->GetStreamDirectoryNumBlocks();
}

uint32_t RawPdbNet::Pdb::GetStreamDirectoryBlocksIndicesSizeInBlocks()
{
	return _pdb->GetStreamDirectoryBlocksIndicesSizeInBlocks();
}

RawPdbNet::SuperBlock^ RawPdbNet::Pdb::GetSuperBlock()
{
	auto nativeSuperBlock = _pdb->GetSuperBlock();
	auto superBlock = gcnew SuperBlock();

	superBlock->fileMagic = gcnew array<Byte>(30);
	for (Int32 i = 0; i < superBlock->fileMagic->Length; i++)
	{
		superBlock->fileMagic[i] = nativeSuperBlock->fileMagic[i];
	}

	superBlock->blockSize = nativeSuperBlock->blockSize;
	superBlock->freeBlockMapIndex = nativeSuperBlock->freeBlockMapIndex;
	superBlock->blockCount = nativeSuperBlock->blockCount;
	superBlock->directorySize = nativeSuperBlock->directorySize;
	superBlock->unknown = nativeSuperBlock->unknown;
	superBlock->directoryStreamBlockIndices = ToManagedArray(
		nativeSuperBlock->directoryBlockIndices,
		GetStreamDirectoryBlocksIndicesSizeInBlocks());

	return superBlock;
}

array<uint32_t>^ RawPdbNet::Pdb::GetDirectoryStreamIndices()
{
	auto& streamDirIndices = _pdb->GetDirectoryStreamIndices();
	return ToManagedArray(streamDirIndices.GetDataAtOffset<uint32_t>(0), GetStreamDirectoryNumBlocks());
}

uint32_t RawPdbNet::Pdb::GetStreamCount()
{
	return _pdb->GetStreamCount();
}

array<int32_t>^ RawPdbNet::Pdb::GetStreamSizes()
{
	return ToManagedArrayCast<uint32_t, int32_t>(_pdb->GetStreamSizes(), GetStreamCount());
}

array<array<uint32_t>^>^ RawPdbNet::Pdb::GetStreamBlocksIndices()
{
	array<array<uint32_t>^>^ streamBlocksIndices = gcnew array<array<uint32_t>^>(GetStreamCount());
	const uint32_t** nativeIndices = _pdb->GetStreamBlocksIndices();
	auto streamSizes = GetStreamSizes();

	for (int i = 0; i < streamBlocksIndices->Length; i++)
	{
		streamBlocksIndices[i] = ToManagedArray(nativeIndices[i], PDB::ConvertSizeToBlockCount(streamSizes[i], _pdb->GetSuperBlock()->blockSize));
	}

	return streamBlocksIndices;
}

RawPdbNet::ErrorCode RawPdbNet::Pdb::CheckFile(array<unsigned char>^ data)
{
	pin_ptr<Byte> p = &data[0];
	return (RawPdbNet::ErrorCode)PDB::ValidateFile(p);
}
