﻿using DC_Lab1.DB.BaseDBContext;
using DC_Lab1.DTO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class StickerControllerUnitTest
    {
        private readonly StickerController _controller;
        private readonly StickerService _service;
        private readonly BaseDbContext _dbContext;
        private readonly IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(["DC_Lab1"]);
        }).CreateMapper();
        public StickerControllerUnitTest()
        {
            _dbContext = new BaseDbContext();
            _service = new StickerService(_mapper, _dbContext);
            _controller = new StickerController(_service)
            { ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() } };


        }

        [TestMethod]
        public void GetUpdate()
        {
            var StickerRequest = new StickerRequestTo(0,  "TestName");
            var Sticker = _controller.CreateSticker(StickerRequest).Result.Value as StickerResponseTo;
            Assert.IsNotNull(Sticker);

            var newSticker = new StickerRequestTo(Sticker.Id, "NewTestName");
            _dbContext.ChangeTracker.Clear();
            var StickerResponce = _controller.UpdateSticker(newSticker).Result.Value as StickerResponseTo;
            Assert.IsNotNull(StickerResponce);
            Assert.AreEqual(StickerResponce.Name, newSticker.Name);
        }
        [TestMethod]
        public void GetEntById()
        {
            var StickerRequest = new StickerRequestTo(0, "TestName");
            var Sticker = _controller.CreateSticker(StickerRequest).Result.Value as StickerResponseTo;
            Assert.IsNotNull(Sticker);
            _dbContext.ChangeTracker.Clear();
            var foundedSticker = _controller.GetStickerById(Sticker.Id).Result.Value as StickerResponseTo;
            Assert.IsNotNull(foundedSticker);
            Assert.AreEqual(foundedSticker.Id, Sticker.Id);
        }
        [TestMethod]
        public async Task GetAllEnt()
        {

            var result = _controller.GetStickers();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<IResponseTo>));
        }
        [TestMethod]
        public async Task CreateEnt()
        {
            var StickerRequest = new StickerRequestTo(0, "TestName");
            var StickerResponse = (await _controller.CreateSticker(StickerRequest)).Value as StickerResponseTo;
            Assert.IsNotNull(StickerResponse);

            Assert.AreEqual(StickerResponse.Name, "TestName");
        }
        [TestMethod]
        public async Task DeleteEnt()
        {
            var StickerRequest = new StickerRequestTo(0, "TestName");
            var StickerResponse = (await _controller.CreateSticker(StickerRequest)).Value as StickerResponseTo;
            Assert.IsNotNull(StickerResponse);

            _dbContext.ChangeTracker.Clear();

            var result = await _controller.DeleteSticker(StickerResponse.Id);

            var expected = typeof(NoContentResult);

            Assert.IsInstanceOfType(result, expected);
        }

    }
}
