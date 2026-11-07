namespace Lotus.Core.RnR
{
    /// <summary>
    /// Тесты для методов пагинации.
    /// </summary>
    public class PageInfoTests
    {
        /// <summary>
        /// Тестирование методов <see cref="XPageInfoHelpers"/>.
        /// </summary>
        [Fact]
        public void ToPageInfo_SplitsTotalSizeIntoPages()
        {
            var pages20100 = XPageInfoHelpers.ToPageInfo(20, 100);

            Assert.Single(pages20100);
            Assert.Equal(0, pages20100[0].PageNumber);
            Assert.Equal(20, pages20100[0].PageSize);

            var pages4110 = XPageInfoHelpers.ToPageInfo(41, 10);

            Assert.Equal(5, pages4110.Length);

            Assert.Equal(0, pages4110[0].PageNumber);
            Assert.Equal(10, pages4110[0].PageSize);

            Assert.Equal(1, pages4110[1].PageNumber);
            Assert.Equal(10, pages4110[1].PageSize);

            Assert.Equal(2, pages4110[2].PageNumber);
            Assert.Equal(10, pages4110[2].PageSize);

            Assert.Equal(3, pages4110[3].PageNumber);
            Assert.Equal(10, pages4110[3].PageSize);

            Assert.Equal(4, pages4110[4].PageNumber);
            Assert.Equal(1, pages4110[4].PageSize);

            var pages5019 = XPageInfoHelpers.ToPageInfo(501, 9);

            Assert.Equal(56, pages5019.Length);

            Assert.Equal(0, pages5019[0].PageNumber);
            Assert.Equal(9, pages5019[0].PageSize);

            Assert.Equal(1, pages5019[1].PageNumber);
            Assert.Equal(9, pages5019[1].PageSize);

            Assert.Equal(55, pages5019[55].PageNumber);
            Assert.Equal(6, pages5019[55].PageSize);
        }

        [Fact]
        public void PageInfoRequest_StoresValues()
        {
            var request = new PageInfoRequest
            {
                PageNumber = 2,
                PageSize = 25,
            };

            Assert.Equal(2, request.PageNumber);
            Assert.Equal(25, request.PageSize);
        }

        [Fact]
        public void PageInfoResponse_StoresValues()
        {
            var response = new PageInfoResponse
            {
                PageNumber = 1,
                PageSize = 10,
                CurrentPageSize = 7,
                TotalCount = 37,
            };

            Assert.Equal(1, response.PageNumber);
            Assert.Equal(10, response.PageSize);
            Assert.Equal(7, response.CurrentPageSize);
            Assert.Equal(37, response.TotalCount);
        }
    }
}
