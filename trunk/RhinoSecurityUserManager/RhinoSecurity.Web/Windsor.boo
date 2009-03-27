import System.Reflection
import Basic.Models
import Basic.Models.Security
import Castle.MonoRail.Framework
import Castle.MonoRail.WindsorExtension
import Rhino.Commons.ForTesting from Rhino.Commons.ActiveRecord

import Rhino.Security
import Rhino.Security.Interfaces from Rhino.Security
import Rhino.Security.Services
import Rhino.Security.ActiveRecord
import Castle.Components.Validator

Facility( "monorail", MonoRailFacility )

facility RhinoSecurityFacility:
	securityTableStructure = SecurityTableStructure.Prefix
	userType = User

# load AR models from assemblies
activeRecordAssemblies = ( 
	Assembly.Load("Basic.Models"), 
	typeof(RegisterRhinoSecurityMappingAttribute).Assembly, 
	typeof(User).Assembly, )

# register Controllers, ViewComponents
webAsm = Assembly.Load("Basic.Web")
for type in webAsm.GetTypes():
        if typeof(Controller).IsAssignableFrom(type):
                Component(type.Name, type)
        elif typeof(ViewComponent).IsAssignableFrom(type):
                Component(type.Name, type)

Component( "active_record_repository", IRepository, ARRepository)

# Rhino.Security Components Registration
component INHibernateInitializationAware, RegisterRhinoSecurityModels
#component IAuthorizationService, AuthorizationService
#component IAuthorizationRepository, AuthorizationRepository

#component IEntityInformationExtractor of Product, ProductInformationExtractor

Component( "active_record_unit_of_work", 
	IUnitOfWorkFactory, 
	ActiveRecordUnitOfWorkFactory, 
	assemblies: activeRecordAssemblies )
