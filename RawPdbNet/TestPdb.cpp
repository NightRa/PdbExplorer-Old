#include "TestPdb.h"

RawPdbNet::ErrorCode RawPdbNet::TestPdb::CheckFile(array<unsigned char>^ data)
{
	pin_ptr<Byte> p = &data[0];
	return (RawPdbNet::ErrorCode)PDB::ValidateFile(p);
}
