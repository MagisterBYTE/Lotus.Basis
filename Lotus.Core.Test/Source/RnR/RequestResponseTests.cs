using System.Collections.Generic;

namespace Lotus.Core.RnR
{
    /// <summary>
    /// Тесты типов запроса и ответа RnR.
    /// </summary>
    public class RequestResponseTests
    {
        [Fact]
        public void Request_StoresPageSortingAndFiltering()
        {
            var request = new Request
            {
                PageInfo = new PageInfoRequest { PageNumber = 1, PageSize = 20 },
                Sorting =
                [
                    new SortByProperty { PropertyPath = "Name", IsDesc = true }
                ],
                Filtering =
                [
                    new FilterByProperty
                    {
                        PropertyPath = "Name",
                        Value = "test",
                        Function = TFilterFunction.Contains,
                    }
                ],
            };

            ILotusRequest lotusRequest = request;

            Assert.Equal(1, request.PageInfo.PageNumber);
            Assert.Equal(20, request.PageInfo.PageSize);
            Assert.Same(request.PageInfo, lotusRequest.PageInfo);
            Assert.Single(request.Sorting!);
            Assert.True(request.Sorting![0].IsDesc);
            Assert.Single(request.Filtering!);
            Assert.Equal(TFilterFunction.Contains, request.Filtering![0].Function);
        }

        [Fact]
        public void Response_SucceedAndFailed_CreateExpectedResults()
        {
            var ok = Response.Succeed("done", 200, "value");
            var failed = Response.Failed("error", 500, "details");

            Assert.NotNull(ok.Result);
            Assert.True(ok.Result!.Succeeded);
            Assert.Equal("done", ok.Result.Message);
            Assert.Equal(200, ok.Result.Code);

            Assert.NotNull(failed.Result);
            Assert.False(failed.Result!.Succeeded);
            Assert.Equal("error", failed.Result.Message);
            Assert.Equal(500, failed.Result.Code);
        }

        [Fact]
        public void ResponseOfT_Succeed_SetsPayload()
        {
            var response = Response<string>.Succeed("payload");

            Assert.Equal("payload", response.Payload);
            Assert.NotNull(response.Result);
            Assert.True(response.Result!.Succeeded);
        }

        [Fact]
        public void ResponsePage_StoresPayloadAndPageInfo()
        {
            var page = new ResponsePage<string>
            {
                Payload = new List<string> { "a", "b" },
                PageInfo = new PageInfoResponse
                {
                    PageNumber = 0,
                    PageSize = 10,
                    CurrentPageSize = 2,
                    TotalCount = 2,
                },
                Result = Result.Ok,
            };

            Assert.Equal(2, page.Payload.Count);
            Assert.Equal(0, page.PageInfo.PageNumber);
            Assert.Equal(2, page.PageInfo.TotalCount);
            Assert.True(page.Result!.Succeeded);
        }

        [Fact]
        public void FilterObject_StoresFilters()
        {
            var filterObject = new FilterObject(
                new FilterByProperty
                {
                    PropertyPath = "Id",
                    Value = "1",
                    Function = TFilterFunction.Equals,
                });

            Assert.Single(filterObject.Filters);
            Assert.Equal("Id", filterObject.Filters[0].PropertyPath);
        }
    }
}
