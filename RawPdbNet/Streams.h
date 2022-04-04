#pragma once
#include <cstdint>

using namespace System;

namespace RawPdbNet
{
	public enum class PdbVersion : uint32_t
	{
		VC2 = 19941610u,
		VC4 = 19950623u,
		VC41 = 19950814u,
		VC50 = 19960307u,
		VC98 = 19970604u,
		VC70Dep = 19990604u,
		VC70 = 20000404u,
		VC80 = 20030901u,
		VC110 = 20091201u,
		VC140 = 20140508u
	};

	public ref struct PdbInfoStreamHeader
	{
		PdbVersion version;
		uint32_t signature;
		uint32_t age;
		Guid^ guid;
	};
}
