namespace MRSample.Models
{
	using Castle.ActiveRecord;

	[ActiveRecord("Student")]
	public class Student : ActiveRecordBase<Student>
	{
		[PrimaryKey]
		public int Id { get; set; }

		[Property]
		public string Name { get; set; }
		
		[Property]
		public string Grade { get; set; }
	}
}
