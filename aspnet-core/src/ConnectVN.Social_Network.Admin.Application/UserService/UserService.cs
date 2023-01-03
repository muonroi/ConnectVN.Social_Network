using ConnectVN.Social_Network.Admin.DTO;
using ConnectVN.Social_Network.Admin.DTO.UserMembers;
using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using ConnectVN.Social_Network.Admin.Infrastructure.Services;
using ConnectVN.Social_Network.Admin.UserContract;
using ConnectVN.Social_Network.Domain;
using ConnectVN.Social_Network.Manage;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Admin.UserService
{
    public class UserService : CrudAppService<IdentityUser, UserMemberDTO, Guid, PagedResultRequestDto, RegisterUpdateUser, RegisterUpdateUser>, IUserService
    {
        private readonly UserManage _usermanage;
        private readonly IRepository<UserMember> _userMemberRepository;
        private readonly IUserServiceAPI _userServiceApi;
        public static readonly List<string> ImageExtensions = new() { "JPG", "JPE", "BMP", "GIF", "PNG", "JPEG" };
        private readonly IBlobContainer<UserUploadAvatar> _userUploadAvatarUpload;
        public UserService(IRepository<IdentityUser, Guid> repository, IRepository<UserMember> userMemberRepository, IUserServiceAPI userServiceApi, UserManage usermanage, IBlobContainer<UserUploadAvatar> userUploadAvatarUpload) : base(repository)
        {
            _userMemberRepository = userMemberRepository;
            _userServiceApi = userServiceApi;
            _usermanage = usermanage;
            _userUploadAvatarUpload = userUploadAvatarUpload;
        }
        private static bool CheckTypeFileIsImage(IFormFile file)
        {
            try
            {
                if (ImageExtensions.Any(x => x.Equals(file.ContentType.Split('/')[1].ToUpperInvariant())))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
        public async override Task<UserMemberDTO> CreateAsync([FromForm] RegisterUpdateUser input)
        {
            try
            {
                UserMember userMember = _usermanage.CreateUser(input);
                UserSigupDTO sigupDTO = new()
                {
                    UserName = input.UserName,
                    EmailAddress = input.Email,
                    Password = input.Password,
                    AppName = input.UserName
                };
                IApiResponse<IdentityUserDto> resultUser = _userServiceApi.RegisterAsync(sigupDTO).GetAwaiter().GetResult();
                if (!resultUser.IsSuccessStatusCode)
                {
                    throw new BusinessException(EnumUserErrorCodes.USR29C.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenRegister);
                }
                string url_img = "";
                if (CheckTypeFileIsImage(input.AvatarFile) && input.AvatarFile.Length > 0)
                {
                    Stream streamImg = input.AvatarFile.OpenReadStream();
                    IApiResponse<string> ressultUpload = _userServiceApi.UploadFileImg(new StreamPart(streamImg, input.AvatarFile.FileName, input.AvatarFile.ContentType)).GetAwaiter().GetResult();
                    url_img = ressultUpload.Content ?? "";
                }
                userMember.Avatar = url_img;
                await _userMemberRepository.InsertAsync(userMember);
                resultUser.Content.Surname = input.LastName;
                resultUser.Content.Name = input.FirstName;
                resultUser.Content.PhoneNumber = input.PhoneNumber;
                return ObjectMapper.Map<IdentityUserDto, UserMemberDTO>(resultUser.Content);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
        public async Task<string> UploadImg([FromForm] IFormFile files)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await files.CopyToAsync(memoryStream).ConfigureAwait(false);
                string id = Guid.NewGuid().ToString() + "." + files.ContentType;
                string filename = $"{ConstName.DefaultNameImg + "_" + files.FileName}";
                await _userUploadAvatarUpload.SaveAsync(filename, memoryStream.ToArray(), overrideExisting: true).ConfigureAwait(false);
                string result = id + '*' + filename;
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
        public async Task<bool> DeleteImg(Guid guidStory)
        {
            try
            {
                UserMember currentFile = await _userMemberRepository.GetAsync(x => x.Id == guidStory);
                string contentType = currentFile.Avatar.Split('.', '*')[1];
                if (currentFile != null)
                {
                    string fileName = currentFile.Avatar.Split('*').LastOrDefault().ToString();
                    return await _userUploadAvatarUpload.DeleteAsync(fileName);
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
        public async Task<FileResult> GetImg(Guid guidStory)
        {
            try
            {
                UserMember currentFile = await _userMemberRepository.GetAsync(x => x.Id == guidStory);
                string contentType = currentFile.Avatar.Split('.', '*')[1];
                if (currentFile != null)
                {
                    string fileName = currentFile.Avatar.Split('*').LastOrDefault().ToString();
                    byte[] myfile = await _userUploadAvatarUpload.GetAllBytesOrNullAsync(fileName);
                    return new FileContentResult(myfile, contentType);
                }

                throw new FileNotFoundException();
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
    }
}
