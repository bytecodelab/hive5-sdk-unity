using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{

    /// <summary>
    /// Hobject.
    /// </summary>
    public class HObject
    {
        public long id { set; get; }
        public string @class { set; get; }
        //public object changes { set; get; }
		public Dictionary<string, ObjectField> Fields { get; private set; }

		public HObject ()
		{
			Fields = new Dictionary<string, ObjectField> ();
		}

		static ObjectField ConvertToObjectField (string key, JsonData jsonData)
		{
			if (jsonData == null)
                return new ObjectFieldNull() { Key = key };

			if (jsonData.IsInt || 
			    jsonData.IsLong)
			{
				return new ObjectFieldLong()
				{
                    Key = key,
					FieldType = ObjectFieldType.Long,
					Value = JsonHelper.ToLong(jsonData, -1),
				};
			}
			else if (jsonData.IsString)
			{
				return new ObjectFieldString()
				{
                    Key = key,
					FieldType = ObjectFieldType.String,
					Value = (string)jsonData,
				};
			}
			else if (jsonData.IsDouble)
			{
				return new ObjectFieldDouble()
				{
                    Key = key,
					FieldType = ObjectFieldType.Double,
					Value = (double)jsonData,
				};
			}
			else if (jsonData.IsArray)
			{
				return new ObjectFieldList()
				{
                    Key = key,
					FieldType = ObjectFieldType.List,
					Value = GetObjectFieldList(jsonData),
				};
			}
			else
			{
				return null;
			}
		}

		static List<ObjectField> GetObjectFieldList (JsonData jsonData)
		{
            List<ObjectField> list = new List<ObjectField>();
            if (jsonData == null ||
                jsonData.IsArray == false)
                return list;

            for (int i = 0; i < jsonData.Count; i++) 
            {
                var jsonDataChild = jsonData[i];
                list.Add(ConvertToObjectField("", jsonDataChild));
            }

            return list;
		}

        /// <summary>
        /// Load the specified json.
        /// </summary>
        /// <param name="json">Json.</param>
        public static HObject Load(JsonData json)
        {
            var id = JsonHelper.ToLong(json, "id", -1);
            var @class = (string)json["class"];
            //var changes = JsonHelper.ToObject(json, "changes", null);

			var hObject = new HObject()
			{
				id = id,
				@class = @class,
				//changes = changes
			};

			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
                if (key == "class" ||
                    key == "id")
                    continue;

				var of = ConvertToObjectField(key, json[key]);
				if (of == null)
				{
					Logger.Log("json is invalid " + key);
					continue;
				}

				hObject.Fields.Add(key, of);
			}

			return hObject;
        }



        /// <summary>
        /// Loads the list.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="json">Json.</param>
        public static List<HObject> LoadList(JsonData json)
        {
            var hobjects = new List<HObject>();

            if (json == null || json.IsArray == false)
                return hobjects;

            var listCount = json.Count;
            for (int currentCount = 0; currentCount < listCount; currentCount++)
            {
                hobjects.Add(HObject.Load(json[currentCount]));
            }

            return hobjects;
        }
    }
}
