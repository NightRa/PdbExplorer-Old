// Copyright 2011-2022, Molecular Matters GmbH <office@molecular-matters.com>
// See LICENSE.txt for licensing details (2-clause BSD License: https://opensource.org/licenses/BSD-2-Clause)

#pragma once
#include <cstdint>

namespace PDB
{
	enum class ErrorCode : uint32_t
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
}
