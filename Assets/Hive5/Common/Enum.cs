using System;

namespace Hive5
{
	/// <summary>
	/// Hive5 API zone.
	/// </summary>
	public enum Hive5APIZone
	{
		Beta = 0,
		Production = 1
	}

	public enum Hive5TimeZone
	{
		UTC = 0,
		KST = 9
	}

	public enum CommandType
	{
		SET = 0,
		INC = 1,
		DEC = -1
	}

	public enum OrderType
	{
		ASC = 0,
		DESC = 1,
	}


}

