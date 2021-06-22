using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOA.Model
{
   public  class RoleModel
    {
        private int _roleid;////角色ID
        private int _rolename; //角色名称（非空）
        private int _rolemsg; ////角色描述（可空）
        private int _rolestate; //-角色状态（0停用 1启用 默认1）
        private int _rolecreatename; //创建人(默认当前用户)
        private int _rolecreatedate;//创建时间（默认当前时间）

        #region //角色ID RoleId
        /// <summary>
        /// //角色ID
        /// </summary>
        public int RoleId
        {
            get { return _roleid; }
            set { _roleid = value; }
        }
        #endregion

        #region  角色名称（非空）RoleName
        /// <summary>
        /// 角色名称（非空）
        /// </summary>
        public int RoleName
        {
            get { return _rolename; }
            set { _rolename = value; }
        }
        #endregion

        #region //角色描述（可空） RoleMsg
        /// <summary>
        /// //角色描述（可空）
        /// </summary>
        public int RoleMsg
        {
            get { return _rolemsg; }
            set { _rolemsg = value; }
        }
        #endregion

        #region 角色状态（0停用 1启用 默认1） RoleState
        /// <summary>
        /// -角色状态（0停用 1启用 默认1）
        /// </summary>
        public int RoleState
        {
            get { return _rolestate; }
            set { _rolestate = value; }
        }
        #endregion

        #region 创建人(默认当前用户) RoleCreateName
        /// <summary>
        /// 创建人(默认当前用户)
        /// </summary>
        public int RoleCreateName
        {
            get { return _rolecreatename; }
            set { _rolecreatename = value; }
        }
        #endregion

        #region 创建时间（默认当前时间） RoleCreateDate
        /// <summary>
        /// 创建时间（默认当前时间）
        /// </summary>
        public int RoleCreateDate
        {
            get { return _rolecreatedate; }
            set { _rolecreatedate = value; }
        }
        #endregion
      
    }
}
