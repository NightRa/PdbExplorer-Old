#pragma once

#include "..\src\PDB.h"
#include "..\src\PDB_RawFile.h"

using namespace System;

namespace RawPdbNet
{
	public enum class ErrorCode: uint32_t
	{
		Success = 0u,

		// main PDB validation
		InvalidSuperBlock,
		InvalidFreeBlockMap,
		UnhandledDirectorySize,

		// stream validation
		InvalidSignature,
		InvalidStreamIndex,
		UnknownVersion
	};

	public ref struct SuperBlock
	{
		array<Byte>^ fileMagic;
		uint32_t blockSize;
		uint32_t freeBlockMapIndex;										// index of the free block map
		uint32_t blockCount;											// number of blocks in the file
		uint32_t directorySize;											// size of the stream directory in bytes
		uint32_t unknown;
		array<uint32_t>^ directoryStreamBlockIndices;
	};

	public ref class Pdb
	{
	public:
		Pdb(array<Byte>^ data);
		~Pdb();

		uint32_t GetStreamDirectoryNumBlocks();       // Number of blocks for the Stream Directory
		uint32_t GetStreamDirectoryBlocksIndicesSizeInBlocks(); // Number of blocks to represent the indices for the Stream Directory Blocks.
		SuperBlock^ GetSuperBlock();
		array<uint32_t>^ GetDirectoryStreamIndices();

		uint32_t GetStreamCount();
		array<int32_t>^ GetStreamSizes();
		array<array<uint32_t>^>^ GetStreamBlocksIndices();

		static ErrorCode CheckFile(array<Byte>^ data);

	private:
		PDB::RawFile* _pdb;
	};
}
