using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseService.Models
{
	public class UniqueString
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[StringLength(5)]
		public string UniqueStrings { get; set; }
	}
}

