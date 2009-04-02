using System;
using Castle.ActiveRecord;

namespace NetTwitt.Model
{
	[ActiveRecord]
	public class Twitt: ActiveRecordBase<Twitt>
	{
		[PrimaryKey]
		public virtual int Id { get; set; }

		[Property(Length = 140)] 
		public virtual string Message{ get; set;}

		[BelongsTo("UserId")] 
		public virtual User User { get; set;}

		[Property]
		public virtual DateTime Posted { get; set; }
	}
}
