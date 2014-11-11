using System;
using System.Linq;
using Summariser.Data.Entities;

namespace Summariser.Data
{
	public class SummariserRepository : ISummariserRepository
	{
		private readonly SummariserContext _context;

		public SummariserRepository(SummariserContext context)
		{
			_context = context;
		}

		public IQueryable<SummaryValue> GetAllValues()
		{
			return _context.SummaryValues;
		}

		public SummaryValue GetValue(Guid id)
		{
			return _context.SummaryValues.SingleOrDefault(v => v.Id.Equals(id));
		}

		public bool SaveAll()
		{
			return _context.SaveChanges() > 0;
		}

		public bool Insert(SummaryValue value)
		{
			try
			{
				_context.SummaryValues.Add(value);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool Delete(Guid id)
		{
			try
			{
				var valueToDelete = _context.SummaryValues.FirstOrDefault(v => v.Id.Equals(id));
				if (valueToDelete != null)
				{
					_context.SummaryValues.Remove(valueToDelete);
					return true;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			return false;
		}
	}
}