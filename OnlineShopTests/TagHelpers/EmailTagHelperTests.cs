﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using OnlineShop.TagHelpers;

namespace OnlineShopTests.TagHelpers
{
    public class EmailTagHelperTests
    {
        [Fact]
        public void Generates_Email_Link()
        {
            // Arrange
            EmailTagHelper emailTagHelper = new() { Address = "test@gmail.com", Content = "Email" };
            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                string.Empty);
            var content = new Mock<TagHelperContent>();

            var tagHelperOutput = new TagHelperOutput("a",
                new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(content.Object));

            // Act
            emailTagHelper.Process(tagHelperContext, tagHelperOutput);

            // Assert
            Assert.Equal("Email", tagHelperOutput.Content.GetContent());
            Assert.Equal("a", tagHelperOutput.TagName);
            Assert.Equal("mailto:test@gmail.com", tagHelperOutput.Attributes[0].Value);
        }
    }
}
