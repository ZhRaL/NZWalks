using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class AddRegionRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Name has to be a minimum of 3 characters")]
    [MaxLength(100, ErrorMessage = "Name has to be a maximum of 3 characters")]
    public string Name { get; set; }
    
    [Required]
    [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
    [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")]
    public string Code { get; set; }
    public string? RegionImageUrl { get; set; }
}