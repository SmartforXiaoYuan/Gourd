﻿using AutoMapper;
using Gourd.Application.IService.Market;
using Gourd.Application.ViewModel.Market;
using Gourd.Domain.Dto.Market;
using Gourd.Domain.IEvent.Market;
using Gourd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gourd.Application.Service.Market
{
    public class UserService : IUserService
    {
        private readonly IUserDomain _userDomain;
        private readonly IMapper _mapper;

        public UserService(IUserDomain userDomain, IMapper mapper)
        {
            _userDomain = userDomain;
            _mapper = mapper;
        }

        public async Task<JsonResponse> CreateUser(CreateUserRequest request)
        {
            UserInfoDto dto = new UserInfoDto()
            {
                Name = request.name,
                Pwd = request.pwd
            };
            return await _userDomain.CreateUser(dto);
        }

        public async Task<JsonResponse> DelUser(List<string> Ids)
        {
            return await _userDomain.DelUser(Ids);
        }

        public async Task<JsonResponse> GetUser(string id)
        {
            return await _userDomain.GetUser(id);
        }

        public async Task<JsonResponse> GetUserList(GetUserListRequest request)
        {
            UserInfoDto dto = new UserInfoDto()
            {
                Name = request.name,
                Pwd = request.pwd,
                sortOrder = request.sortOrder,
                pageIndex = request.pageIndex.Value,
                pageSize = request.pageSize.Value
            };
            return await _userDomain.GetUserList(dto);
        }

        public async Task<JsonResponse> UpdateUser(UpdateUserRequest request)
        {
            var response = await _userDomain.GetUser(request.id);
            if (response.status != 0) return response;
            var dto = _mapper.Map<UserInfoDto>(response.data);
            dto.Name = request.name;
            dto.Pwd = request.pwd;
            return await _userDomain.UpdateUser(dto);
        }
    }
}
