using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AliHotel.Database;
using AliHotel.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AliHotel.Identity
{
    public class IdentityStore : IUserLockoutStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>
    {
        private readonly DatabaseContext _context;

        /// <summary/>
        public IdentityStore(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary/>
        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary/>
        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Получение роли
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            var result = (IList<string>)new List<string>() { user.RoleId.ToString() };
            return Task.FromResult(result);
        }
        /// <summary>
        /// Проверка пользователя на налицие роли
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="roleName">Название роли</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.RoleId.ToString() == roleName);
        }
        /// <summary>
        /// Получение коллекции пользователей находящихся в роли
        /// </summary>
        /// <param name="roleName">Название роли</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #region CRUD
        /// <summary/>
        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary/>
        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary/>
        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            DateTimeOffset? result = null;
            return Task.FromResult(result);
        }
        /// <summary>
        /// Установка даты окончания блокировки
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="lockoutEnd">Дата окончания блокировки</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
        /// <summary>
        /// Инкремент счетчика неудачных попыток входа
        /// </summary>
        /// <param name="user">Пользоваетль</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
        /// <summary>
        /// Сброс счетчика неудачных попыток входа
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
        /// <summary>
        /// Получение количества неудачных попыток входа
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
        /// <summary>
        /// Проверка установленности блокировки пользоваетля
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }
        /// <summary>
        /// Установка блокировки пользоваетля
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="enabled">Включение блокировки</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
        /// <summary>
        /// Полчение идентификатор пользователя
        /// </summary>
        /// <param name="user">Пользоваетль</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }
        /// <summary>
        /// Получение имени пользоваетля
        /// </summary>
        /// <param name="user">Пользоваетль</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }
        /// <summary/>
        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Получение нормализованного имени пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email.ToUpper());
        }
        /// <summary/>
        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Нахождение пользователя по идентификатору
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var guid = Guid.Parse(userId);
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == guid, cancellationToken);
            return user;
        }
        /// <summary>
        /// Нахождение пользователя по Email
        /// </summary>
        /// <param name="normalizedUserName">Нормализованное имя пользователя</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            normalizedUserName = normalizedUserName.ToUpper();
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Email.ToUpper() == normalizedUserName, cancellationToken);
            return user;
        }
        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
        /// <summary/>
        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Получение хэша пароля
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }
        /// <summary>
        /// Проверка наличия пароля
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
