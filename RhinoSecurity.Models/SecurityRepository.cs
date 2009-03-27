namespace RhinoSecurity.Models
{
	using System;
	using System.Collections.Generic;
	using System.Security.Cryptography;
	using System.Text;
	using NHibernate.Criterion;
	using Rhino.Commons;

	public static class SecurityRepository
	{

		public static void CreateUser(User user)
		{
			user.Password = HashPassword(user.Password);
			Repository<User>.Save(user);
		}

		public static void ChangePassword(string email, string oldPassword, string newPassword)
		{
			using(UnitOfWork.Start())
			{
				User user = Authenticate(email, oldPassword);
				if (user != null)
				{
					user.Password = HashPassword(newPassword);
					Repository<User>.Update(user);
				}
				else
				{
					throw new Exception("Unable to change your password; make sure you provide the old password.");
				}
			}
		}

		public static ICollection<User> FindAllUser(string q, bool disabled)
		{
			DetachedCriteria detachedCriteria = DetachedCriteria.For<User>()
				.Add(Restrictions.Like("Name", q, MatchMode.Anywhere))
				.Add(Restrictions.Eq("Disabled", disabled));

			return Repository<User>.FindAll(detachedCriteria, Order.Asc("Name"));
		}

		public static ICollection<User> FindAllUser(string q)
		{
			DetachedCriteria detachedCriteria = DetachedCriteria.For<User>()
				.Add(Restrictions.Like("Name", q, MatchMode.Anywhere));

			return Repository<User>.FindAll(detachedCriteria, Order.Asc("Name"));			
		}
		
		public static ICollection<User> FindAllUser()
		{
			return Repository<User>.FindAll();
		}

		public static User GetUserByEmail(string email)
		{
			return Repository<User>.FindOne(Restrictions.Eq("Email", email));
		}

		public static User Authenticate(string email, string passwrod)
		{
			DetachedCriteria detachedCriteria = DetachedCriteria.For<User>()
				.Add(Restrictions.Eq("Email", email))
				.Add(Restrictions.Not(Expression.Eq("Email", "anon@anonymous")))	// do not allow anonymous login
				.Add(Restrictions.Eq("Password", HashPassword(passwrod)));

			return Repository<User>.FindOne(detachedCriteria);
		}

		private static string HashPassword(string passwrod)
		{
			SHA1 sha1 = SHA1.Create();
			byte[] salted = sha1.ComputeHash(UnicodeEncoding.UTF8.GetBytes(passwrod));
			return UnicodeEncoding.UTF8.GetString(salted);
		}

		public static User GetAnonymousUser()
		{
			User user = Repository<User>.FindOne(Expression.Eq("Email", "anon@anonymous"));
			if (user == null)
			{
				user = new User("Anonymous", "anon@anonymous", "DO_NOT_ALLOW_TO_LOGIN");
				CreateUser(user);
			}
			return user;
		}

	}
}