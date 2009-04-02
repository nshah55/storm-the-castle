using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Castle.ActiveRecord;
namespace NetTwitt.Model
{
	[ActiveRecord("TwitterUser")]
	public class User : ActiveRecordBase<User>
	{
		[PrimaryKey]
		public int Id { get; set; }

		[Property(NotNull = true), ValidateIsUnique]
		public string Username { get; set; }

		[Property(NotNull = true)]
		public string Password { get; set; }

		[Property(NotNull = true)]
		public string Email { get; set; }

		[Property]
		public virtual string Name { get; set; }

		[Property(Length = 140)]
		public virtual string Biog { get; set; }

		[Property]
		public virtual string Picture { get; set; }

		private IList<Twitt> m_Twitts;
		[HasMany(typeof(Twitt), Cascade = ManyRelationCascadeEnum.SaveUpdate)]
		public virtual IList<Twitt> Twitts
		{
			get
			{
				if (null == m_Twitts)
					m_Twitts = new List<Twitt>();
				return m_Twitts;
			}
			set { m_Twitts = value; }
		}

		private IList<User> m_Follows;
		[HasAndBelongsToMany(typeof(User),// Cascade = ManyRelationCascadeEnum.SaveUpdate, 
			Table = "Follows", ColumnKey = "UserId", ColumnRef = "Follows")]
		public virtual IList<User> Follows
		{
			get
			{
				if (null == m_Follows)
					m_Follows = new List<User>();
				return m_Follows;
			}
			set { m_Follows = value; }
		}

		private IList<User> m_Followers;
		[HasAndBelongsToMany(typeof(User), //Cascade = ManyRelationCascadeEnum.SaveUpdate, //Lazy = true, 
			Inverse = true, Table = "Follows", ColumnKey = "Follows", ColumnRef = "UserId")]
		public virtual IList<User> Followers
		{
			get
			{
				if (null == m_Followers)
					m_Followers = new List<User>();
				return m_Followers;
			}
			set { m_Followers = value; }
		}
	}
}
