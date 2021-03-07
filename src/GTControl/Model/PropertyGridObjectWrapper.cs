using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTControl
{
    public class PropertyGridObjectWrapper : CustomTypeDescriptor
	{
		private object _selectedObject;
		List<PropertyDescriptor> _descriptors;

        #region Constructor
        public PropertyGridObjectWrapper(object obj) : base(TypeDescriptor.GetProvider(obj).GetTypeDescriptor(obj))
		{
			_descriptors = new List<PropertyDescriptor>();
			SelectedObject = obj;
		}
        #endregion

        #region Properties
        public object SelectedObject
		{
			get { return _selectedObject; }
			set
			{
				if (_selectedObject == value) return;

				if (value != null)
				{
					_descriptors.Clear();

					var propertyNames = ReflectionUtil.GetBrowsablePropertyNames(value, "Page Option");
					if (propertyNames != null && propertyNames.Count > 0)
					{
						var allproperties = TypeDescriptor.GetProperties(value);
						foreach (string propertyname in propertyNames)
						{
							var property = allproperties.Find(propertyname, false);
							if (property == null) continue;

							if (!_descriptors.Contains(property)) _descriptors.Add(property);
						}
					}
				}

				_selectedObject = value;
			}
		}
        #endregion

        #region Public Method
        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return GetProperties();
		}

		public override PropertyDescriptorCollection GetProperties()
		{
			return new PropertyDescriptorCollection(_descriptors.ToArray(), true);
		}
        #endregion
    }
}
