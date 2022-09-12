﻿using Microsoft.AspNetCore.Mvc;
using SectorsBackend.DTOs;

namespace SectorsBackend.Repositories.Interfaces
{
	public interface ISectorsRepository
	{
		public Task<ActionResult<List<SectorDTO>>> GetSectorsFilteredByCategoryAsync();
	}
}