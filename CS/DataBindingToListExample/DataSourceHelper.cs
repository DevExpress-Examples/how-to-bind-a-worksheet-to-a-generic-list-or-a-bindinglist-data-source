using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace DataBindingToListExample {
    public static class DataSourceHelper {
        public static List<T> GetDataSouresFromXml<T>(string fileName, string attribute) where T : class {
            if (!File.Exists(fileName))
                return null;
            using (Stream stream = File.OpenRead(fileName)) {
                XmlSerializer s = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(attribute));
                return (List<T>)s.Deserialize(stream);
            }
        }
        public static PropertyDescriptorCollection GetSourceProperties(object list) {
            ITypedList typedList = list as ITypedList;
            if (typedList != null)
                return typedList.GetItemProperties(null);
            IList l = list as IList;
            if (l != null && l.Count > 0)
                return GetItemProperties(l[0]);
            IEnumerable enumerable = list as IEnumerable;
            if (enumerable != null) {
                IEnumerator enumerator = enumerable.GetEnumerator();
                if (enumerator.MoveNext())
                    return GetItemProperties(enumerator.Current);
            }
            return null;
        }
        private static PropertyDescriptorCollection GetItemProperties(object item) {
            PropertyDescriptorCollection col = TypeDescriptor.GetProperties(item);
            if (col == null || col.Count == 0)
                return null;
            return col;
        }

    }
}
