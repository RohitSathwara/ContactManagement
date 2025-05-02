$('.favorite-toggle').on('click', function (e) {
	var id = $(this).data('id');
	var isFavorite = $(this).data('is-favorite');
	var thisData = $(this);

	$.ajax({
		url: '/Contact/ToggleFavorite/' + id,
		type: 'POST',
		success: function (data) {
			if (data.success) {
				if (data.isFavorite) {
					thisData.html('<span class="text-warning">&#9733;</span>');
				} else {
					thisData.html('<span class="text-muted">&#9734;</span>');
				}
				thisData.data('is-favorite', data.isFavorite);

			} else if (data.error) {
				alert(data.error);
			}
		},
		error: function () {
			alert('An error occurred while toggling the favorite status.');
		}
	});
});