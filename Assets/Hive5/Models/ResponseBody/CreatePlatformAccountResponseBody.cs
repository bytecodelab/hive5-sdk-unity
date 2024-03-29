// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using Hive5.Models;
using LitJson;

namespace Hive5.Models
{
	public class CreatePlatformAccountResponseBody : IResponseBody
		{	
			public User User { get; set; }

			/// <summary>
			/// Load the specified json.
			/// </summary>
			/// <param name="json">Json.</param>
			public static IResponseBody Load(JsonData json)
			{
				if (json == null)
					return null;

				return new CreatePlatformAccountResponseBody() {
                    User = new User()
                    {
                        platform = (string)json["user"]["platform"],
                         id = (string)json["user"]["id"],
                    },
				};
			}
		}
}

