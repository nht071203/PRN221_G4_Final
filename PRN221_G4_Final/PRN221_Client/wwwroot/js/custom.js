function expandSearch() {
    var headSearch = document.querySelector('.head-search');
    var searchSuggestions = document.getElementById('searchSuggestions');
    var searchContainer = document.querySelector('.search-container');

    headSearch.classList.add('expand');
    searchSuggestions.classList.add('showSuggest');
    searchContainer.classList.add('showBoxShadow');
}

function collapseSearch() {
    var headSearch = document.querySelector('.head-search');
    var searchSuggestions = document.getElementById('searchSuggestions');
    var searchContainer = document.querySelector('.search-container');

    headSearch.classList.remove('expand');
    searchSuggestions.classList.remove('showSuggest');
    searchContainer.classList.remove('showBoxShadow');
}

function exitSearch() {
    var searchInput = document.getElementById('searchInput');
    searchInput.blur();  // Remove focus from the search input
}

//POP UP ADD POST 
const imageInput = document.getElementById('imageInput');
const imagePreview = document.getElementById('imagePreview');
const clearImagesButton = document.getElementById('clearImagesButton');

// Mảng để lưu các file đã chọn
let selectedFiles = [];

// Sự kiện cho nút chọn ảnh
document.getElementById('selectImageButton').addEventListener('click', () => {
    imageInput.click();
});

// Sự kiện cho input file
imageInput.addEventListener('change', (event) => {
    const files = Array.from(event.target.files);

    // Duyệt qua từng file và thêm vào danh sách đã chọn
    files.forEach(file => {
        // Thêm file vào danh sách đã chọn
        selectedFiles.push(file); // Thêm file vào danh sách đã chọn

        // Tạo và thêm ảnh mới vào preview
        const reader = new FileReader();
        reader.onload = function (e) {
            const img = document.createElement('img');
            img.src = e.target.result;
            img.style.width = '100px'; // Kích thước hình ảnh
            img.style.marginRight = '10px';
            imagePreview.appendChild(img);
        };
        reader.readAsDataURL(file);
    });

    // Hiển thị nút xóa ảnh nếu có ảnh
    clearImagesButton.style.display = selectedFiles.length > 0 ? 'block' : 'none';

    // Đặt lại giá trị của input file để có thể chọn lại cùng file
    imageInput.value = '';
});

// Sự kiện cho nút xóa ảnh
clearImagesButton.addEventListener('click', () => {
    imagePreview.innerHTML = ''; // Xóa ảnh
    selectedFiles = []; // Xóa danh sách file đã chọn
    clearImagesButton.style.display = 'none'; // Ẩn nút xóa ảnh
});


document.addEventListener('DOMContentLoaded', () => {
    const postInput = document.getElementById('postInput');
    const popup = document.getElementById('popup');
    const closePopupBtn = document.getElementById('closePopupBtn');

    // Đảm bảo popup bị ẩn khi khởi động
    popup.style.display = 'none';

    postInput.addEventListener('click', () => {
        popup.style.display = 'flex';
    });

    closePopupBtn.addEventListener('click', () => {
        popup.style.display = 'none';
    });
});