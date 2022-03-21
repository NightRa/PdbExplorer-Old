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

	public ref class TestPdb
	{
	public:
		ErrorCode CheckFile(array<Byte>^ data);
	};
}
