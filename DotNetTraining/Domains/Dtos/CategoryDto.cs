<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;

namespace DotNetTraining.Domains.Dtos
=======
﻿namespace DotNetTraining.Domains.Dtos
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
<<<<<<< HEAD
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
=======
>>>>>>> d9e5241c542531088d3b70cd4b4149e8b78c996e
        public string Name { get; set; }
    }
}
