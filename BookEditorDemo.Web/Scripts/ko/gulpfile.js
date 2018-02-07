var gulp = require('gulp');
var browserify = require('browserify');
var source = require('vinyl-source-stream');
var buffer = require('vinyl-buffer');
var composer = require('gulp-uglify/composer');
var uglify = require('uglify-es');
var plumber = require('gulp-plumber');
var minify = composer(uglify, console);
var rename = require('gulp-rename');

var browserSync = require('browser-sync').create();


gulp.task('js', function () {
    var b = browserify({
        entries: './src/app.js',
        debug: true
    });

    return b.bundle().
        pipe(plumber(console.log.bind(console))).
        pipe(source('index.js')).
        pipe(gulp.dest('./build/')).
		pipe(gulp.dest('./../'));
        // pipe(buffer()).
        // pipe(rename('index.min.js')).
        // pipe(minify()).
        // pipe(gulp.dest('./build/'));
});



gulp.task('watch', function () {

    browserSync.init({
       proxy: 'localhost/'
    });

    gulp.watch(['./src/**/*.js'], ['js']);
    
    gulp.watch('./build/*.js').on('change', browserSync.reload);
	gulp.watch('./*.html').on('change', browserSync.reload);
	gulp.watch('./css/*.css').on('change', browserSync.reload);

});

gulp.task('default', ['js', 'watch']);