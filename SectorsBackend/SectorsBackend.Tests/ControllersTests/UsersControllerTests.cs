using Moq;
using SectorsBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectorsBackend.UnitTests.ControllersTests
{
	public class UsersControllerTests
	{
		private readonly Mock<IUsersRepository> usersRepository;
		public UsersControllerTests()
		{
			usersRepository = new Mock<IUsersRepository>();
		}
	}
}
