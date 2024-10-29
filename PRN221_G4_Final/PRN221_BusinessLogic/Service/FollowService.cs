using Microsoft.Identity.Client;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using PRN221_Repository.AccountRepo;
using PRN221_Repository.FollowRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;
        private readonly IAccountRepository _accountRepository;

        public FollowService(IFollowRepository followRepository, IAccountRepository accountRepository)
        {
            _followRepository = followRepository;
            _accountRepository = accountRepository;
        }

        public async Task<List<Account>> ListFollowers(int id)
        {
            var follows = await _followRepository.GetFollowingByAccountId(id);
            return await _accountRepository.GetAccountsByIds(follows.Select(f => f.BeFollowedId).ToList());
        }

        public async Task<List<Account>> ListFollowing(int id)
        {
            var follows = await _followRepository.GetFollowingByAccountId(id);
            return await _accountRepository.GetAccountsByIds(follows.Select(f => f.BeFollowedId).ToList());
        }

        public async Task<List<Account>> ListFriends(int id)
        {
            var follows = await _followRepository.GetFriendsByAccountId(id);
            return await _accountRepository.GetAccountsByIds(follows.Select(f => f.BeFollowedId).ToList());
        }
    }
}
