using BookStoreApp.Blazor.Server.UI.Services.Author;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using BookStoreApp.Blazor.Server.UI.Services.Book;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BookStoreApp.Blazor.Server.UI.Pages.Books
{
    public partial class Create
    {
        [Inject] IBookService bookService { get; set; }
        [Inject] IAuthorService authorService { get; set; }
        [Inject] NavigationManager navManager { get; set; }

        private BookCreateDto BookModel = new();
        private List<AuthorReadOnlyDto> Authors = new();

        private string AcceptedFileFormats = "image/png,image/jpg,image/webp";

        private string UploadFileWarning = string.Empty;
        private string img = string.Empty;
        private long maxFileSize = 1024 * 1024 * 5;

        protected override async Task OnInitializedAsync()
        {
            var response = await authorService.Get();
            if (response.Success)
            {
                Authors = response.Data;
            }
        }

        private async Task BookCreateHandler()
        {
            var response = await bookService.Create(BookModel);
            if (response.Success)
            {
                BackToList();
            }
        }
        private async Task FileSelectionHandler(InputFileChangeEventArgs eventArgs)
        {
            var file = eventArgs.File;
            if (file != null)
            {
                if (file.Size > maxFileSize)
                {
                    UploadFileWarning = "This file is too big for upload.";
                    return;
                }

                var ext = System.IO.Path.GetExtension(file.Name);
                if (!(ext.ToLower().Contains("jpg") || ext.ToLower().Contains("png") || ext.ToLower().Contains("jpeg") || ext.ToLower().Contains("webp")))
                {
                    UploadFileWarning = "Please select a valid image file (*.jpg | *.png | *.webp)";
                    return;
                }

                var byteArray = new byte[file.Size];
                await file.OpenReadStream().ReadAsync(byteArray);

                string imageType = file.ContentType;
                string base64string = Convert.ToBase64String(byteArray);

                // Populate DTO
                BookModel.ImageData = base64string;
                BookModel.ImageName = file.Name;

                // img preview
                img = $"data:{imageType};base64,{base64string}";
            }
        }

        private void BackToList()
        {
            navManager.NavigateTo("/books/");
        }
    }
}
