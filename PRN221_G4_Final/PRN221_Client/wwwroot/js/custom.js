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