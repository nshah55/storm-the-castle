namespace MRSample.Controllers
{
	using System;
	using Castle.MonoRail.ActiveRecordSupport;
	using Castle.MonoRail.Framework;
	using Models;

	/// <summary>
	/// This is a sample CRUD controller
	/// observe how object loaded, create, remove etc.
	/// </summary>
	public class StudentController: SmartDispatcherController
	{

		public void Index()
		{
			PropertyBag["students"] = Student.FindAll();
		}

		public void Add()
		{
			PropertyBag["student"] = new Student();
		}

		public void Create([ARDataBind("student")] Student student)
		{
			try
			{
				student.Save();

				// Alright, save completed.
				Flash["msg"] = "Student created";

				// Hmm, let move to student index page
				RedirectToAction("index");

				// stop processing the view -> tell MonoRail do not try to find view to process
				CancelView();
			}
			catch(Exception ex)
			{
				Flash["error"] = ex.Message;

				// throw back the object for modification
				Flash["student"] = student;

				// Manually pickup view template otherwise
				// MonoRail find it default view template.
				RenderView("add");
			}

		}

		public void Edit([ARFetch("id")] Student student)
		{
			PropertyBag["student"] = student;
		}

		public void Update([ARDataBind("student")] Student student)
		{
			try
			{
				student.Update();
				
				Flash["msg"] = "Student updated";

				RedirectToAction("index");

				CancelView();
			}
			catch(Exception ex)
			{
				Flash["error"] = ex.Message;

				Flash["student"] = student;

				RenderView("edit");
			}
		}

		public void Remove([ARFetch("id")] Student student, bool confirm)
		{
			if(confirm)
			{
				student.Delete();

				Flash["msg"] = "Student deleted";

				RedirectToAction("index");
				CancelView();
			}
			else
			{
				PropertyBag["student"] = student;	
			}			
		}

	}
}
