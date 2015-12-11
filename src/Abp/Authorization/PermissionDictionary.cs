using System.Collections.Generic;
using System.Linq;

namespace Abp.Authorization
{
    /// <summary>
    /// Used to store and manipulate dictionary of permissions.
    /// 权限字典，用于存储和操作权限字典
    /// </summary>
    internal class PermissionDictionary : Dictionary<string, Permission>
    {
        /// <summary>
        /// Adds all child permissions of current permissions recursively.
        /// 添加全部权限，递归所有自权限
        /// </summary>
        public void AddAllPermissions()
        {
            foreach (var permission in Values.ToList())
            {
                AddPermissionRecursively(permission);
            }
        }

        /// <summary>
        /// Adds a permission and it's all child permissions to dictionary.
        /// 递归添加权限，添加一个权限和他的子权限
        /// </summary>
        /// <param name="permission">Permission to be added 权限</param>
        private void AddPermissionRecursively(Permission permission)
        {
            //Prevent multiple adding of same named permission.
            //防止多个相同命名权限的添加
            Permission existingPermission; //现有权限
            if (TryGetValue(permission.Name, out existingPermission))
            {
                //如果TryGetValue获取的权限与当前权限不同，则存在重复的权限名称
                if (existingPermission != permission)
                {
                    //检测到重复的权限名称
                    throw new AbpInitializationException("Duplicate permission name detected for " + permission.Name);                    
                }
            }
            else
            {
                this[permission.Name] = permission;
            }

            //Add child permissions (recursive call)
            //添加子权限（递归）
            foreach (var childPermission in permission.Children)
            {
                AddPermissionRecursively(childPermission);
            }
        }
    }
}