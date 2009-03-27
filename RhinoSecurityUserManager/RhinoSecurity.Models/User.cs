namespace RhinoSecurity.Models
{
	using System;
	using System.Collections.Generic;
	using System.Security.Principal;
	using Castle.ActiveRecord;
	using Castle.Components.Validator;
	using Rhino.Security;
	using Rhino.Security.Model;

	[ActiveRecord("App_Users", Cache = CacheEnum.ReadWrite)]
	public class User : IUser, IPrincipal, IIdentity
	{
		private long id;
		private string name = "Anonymous";
		private string email = "anon@anonymous";
		private string mobile;
		private string password;
		private IList<UsersGroup> roles = new List<UsersGroup>();
		private DateTime createdOn = DateTime.Now;
		private DateTime? lastAccessOn;
		private string lastAccessIP;
		private bool disabled = false;

		public User()
		{
		}

		public User(string name, string email, string password)
		{
			this.name = name;
			this.email = email;
			this.password = password;
		}

		[PrimaryKey(PrimaryKeyType.Identity)]
		public virtual long Id
		{
			get { return id; }
			set { id = value; }
		}

		[Property(NotNull = true)]
		public virtual string Name
		{
			get { return name; }
			set { name = value; }
		}

		[HasAndBelongsToMany(typeof(UsersGroup), 
			ColumnKey = "UserId", ColumnRef = "GroupId", Table = "UsersToUsersGroups",
			Lazy = true, Inverse = true)]
		public virtual IList<UsersGroup> Roles
		{
			get { return roles; }
			set { roles = value; }
		}

		[Property(NotNull = true, Length = 65)]
		[ValidateIsUnique, ValidateEmail]
		public string Email
		{
			get { return email; }
			set { email = value; }
		}

		[Property(NotNull = false, Length = 20)]
		public string Mobile
		{
			get { return mobile; }
			set { mobile = value; }
		}

		[Property(NotNull = true, Length = 250)]
		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		[Property]
		public virtual DateTime CreatedOn
		{
			get { return createdOn; }
			set { createdOn = value; }
		}

		[Property]
		public virtual DateTime? LastAccessOn
		{
			get { return lastAccessOn; }
			set { lastAccessOn = value; }
		}

		[Property]
		public virtual string LastAccessIP
		{
			get { return lastAccessIP; }
			set { lastAccessIP = value; }
		}

		[Property]
		public virtual bool Disabled
		{
			get { return disabled; }
			set { disabled = value; }
		}

		/// <summary>
		/// Gets or sets the security info for this user
		/// </summary>
		/// <value>
		/// The security info.
		/// </value>
		public SecurityInfo SecurityInfo
		{
			get { return new SecurityInfo(name, id); }
		}

		#region IPrincipal Member

		/// <summary>
		///  Determines whether the current principal belongs to the specified role.
		/// </summary>
		/// <returns>
		/// true if the current principal is a member of the specified role; otherwise, false.
		/// </returns>
		/// <param name="role">
		/// The name of the role for which to check membership. 
		/// </param>
		public bool IsInRole(string role)
		{
			// we are going to use single level group.
			foreach(UsersGroup usersGroup in roles)
			{
				if (usersGroup.Name.Equals(role)) return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the identity of the current principal.
		/// </summary>
		/// <returns>
		/// The <see cref="T:System.Security.Principal.IIdentity" /> object associated with the current principal.
		/// </returns>
		public IIdentity Identity
		{
			get { return new GenericIdentity(name);; }
		}

		#endregion

		#region IIdentity Members

		/// <summary>
		/// Gets the type of authentication used.
		/// </summary>
		/// <returns>
		/// The type of authentication used to identify the user.
		/// </returns>
		public string AuthenticationType
		{
			get { return "RhinoSecurityFilter"; }
		}

		/// <summary>
		/// Gets a value that indicates whether the user has been authenticated.
		/// </summary>
		/// <returns>
		/// true if the user was authenticated; otherwise, false.
		/// </returns>
		public bool IsAuthenticated
		{
			get { return !name.Equals("Anonymous"); }
		}

		#endregion

	}
}