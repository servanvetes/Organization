using Entities.Models;
using Proje1.DTOs;

namespace Proje1.Operations
{
    public static class EntConversions
    {
        public static User EntityToDto(UserDto userDto)
        {
            return new User();
        }
    }
}
