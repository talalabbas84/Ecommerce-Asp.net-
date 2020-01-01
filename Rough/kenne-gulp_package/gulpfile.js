var gulp = require('gulp'),
	clean = require('gulp-clean'),
	browserSync = require('browser-sync').create(),
	concat = require('gulp-concat'),
	sass = require('gulp-sass'),
	autoprefixer = require('gulp-autoprefixer'),
	uglifycss = require('gulp-uglifycss'),
	uglify = require('gulp-uglify'),
	notify = require('gulp-notify'),
	plumber = require('gulp-plumber'),
	sourcemaps = require('gulp-sourcemaps'),
	fileInclude = require('gulp-file-include'),
	beautifyCode = require('gulp-beautify-code'),
	imagemin = require('gulp-imagemin'),
	imageminUPNG = require('imagemin-upng'),
	mozjpeg = require('imagemin-mozjpeg'),
	jpegRecompress = require('imagemin-jpeg-recompress'),
	svgo = require('imagemin-svgo'),
	// Source Folder Locations
	src = {
		root: './src/',

		rootHtml: './src/*.html',
		rootPartials: './src/partials/',

		rootFonts: './src/assets/fonts/*',
		fontsAll: './src/assets/fonts/**/*',

		rootVendorCss: './src/assets/css/vendor/*.css',
		rootPluginsCss: './src/assets/css/plugins/*.css',

		styleScss: './src/assets/scss/style.scss',
		rootScss: './src/assets/scss/*',
		scssAll: './src/assets/scss/**/*',

		rootVendorJs: './src/assets/js/vendor/*.js',
		rootPluginsJs: './src/assets/js/plugins/*.js',

		pluginsJsRelFolder: './src/assets/js/plugins/plugins-related/**/*',
		pluginsJsRelFolderRoot: './src/assets/js/root-plugins-related/**/*',

		mainJs: './src/assets/js/main.js',

		ajaxMail: './src/assets/js/ajax-mail.js',

		images: './src/assets/images/**/*',

		rootPhp: './src/assets/php/*',
		phpAll: './src/assets/php/**/*'
	},
	// Destination Folder Locations
	dest = {
		root: './dest/',
		fonts: './dest/assets/fonts/',
		assets: './dest/assets/',
		scss: './dest/assets/scss/',

		rootCss: './dest/assets/css',
		rootVendorCss: './dest/assets/css/vendor/',
		rootPluginsCss: './dest/assets/css/plugins/',

		rootJs: './dest/assets/js',
		rootAjaxMail: './dest/assets/ajax-mail.js',
		rootVendorJs: './dest/assets/js/vendor/',
		rootPluginsJs: './dest/assets/js/plugins/',

		rootPhp: './dest/assets/php/',

		images: './dest/assets/images/'
	},
	// Separator For Vendor CSS & JS
	separator = '\n\n/*====================================*/\n\n',
	// Autoprefixer Options
	autoPreFixerOptions = [
		'last 4 version',
		'> 1%',
		'ie >= 9',
		'ie_mob >= 10',
		'ff >= 30',
		'chrome >= 34',
		'safari >= 7',
		'opera >= 23',
		'ios >= 7',
		'android >= 4',
		'bb >= 10'
	];

/*-- 
    Live Synchronise & Reload
--------------------------------------------------------------------*/

// Browser Synchronise
function liveBrowserSync(done) {
	browserSync.init({
		server: {
			baseDir: dest.root
		}
	});
	done();
}
// Reload
function reload(done) {
	browserSync.reload();
	done();
}

/*-- 
    Gulp Custom Notifier
--------------------------------------------------------------------*/
function customPlumber(errTitle) {
	return plumber({
		errorHandler: notify.onError({
			title: errTitle || 'Error running Gulp',
			message: 'Error: <%= error.message %>',
			sound: 'Glass'
		})
	});
}

/*-- 
    Gulp Other Tasks
--------------------------------------------------------------------*/

/*-- Remove Destination Folder Before Starting Gulp --*/
function cleanProject(done) {
	gulp.src(dest.root).pipe(customPlumber('Error On Clean App')).pipe(clean());
	done();
}

/*-- Copy Font Form Source to Destination Folder --*/
function fonts(done) {
	gulp.src(src.rootFonts).pipe(customPlumber('Error On Copy Fonts')).pipe(gulp.dest(dest.fonts));
	done();
}

/*-- 
    All HTMl Files Compile With Partial & Copy Paste To Destination Folder
--------------------------------------------------------------------*/
function html(done) {
	gulp
		.src(src.rootHtml)
		.pipe(customPlumber('Error On Compile HTML'))
		.pipe(fileInclude({ basepath: src.rootPartials }))
		.pipe(
			beautifyCode({
				indent_size: 4,
				indent_char: ' ',
				max_preserve_newlines: 1,
				unformatted: [ 'code', 'pre', 'em', 'strong', 'span', 'i', 'b', 'br' ]
			})
		)
		.pipe(gulp.dest(dest.root));
	done();
}

/*-- 
    CSS & SCSS Task
--------------------------------------------------------------------*/

/*-- Vendor CSS --*/
function vendorCss(done) {
	gulp
		.src(src.rootVendorCss)
		.pipe(customPlumber('Error On Copying Vendor CSS'))
		.pipe(gulp.dest(dest.rootVendorCss))
		.pipe(customPlumber('Error On Combining Vendor CSS'))
		.pipe(concat('vendor.css', { newLine: separator }))
		.pipe(autoprefixer(autoPreFixerOptions))
		.pipe(gulp.dest(dest.rootVendorCss))
		.pipe(customPlumber('Error On Combine & Minifying Vendor CSS'))
		.pipe(concat('vendor.min.css', { newLine: separator }))
		.pipe(uglifycss())
		.pipe(autoprefixer(autoPreFixerOptions))
		.pipe(gulp.dest(dest.rootVendorCss));
	done();
}

/*-- All CSS Plugins --*/
function pluginsCss(done) {
	gulp
		.src(src.rootPluginsCss)
		.pipe(customPlumber('Error On Copying Plugins CSS'))
		.pipe(gulp.dest(dest.rootPluginsCss))
		.pipe(customPlumber('Error On Combining Plugins CSS'))
		.pipe(concat('plugins.css', { newLine: separator }))
		.pipe(autoprefixer(autoPreFixerOptions))
		.pipe(gulp.dest(dest.rootPluginsCss))
		.pipe(customPlumber('Error On Combine & Minifying Plugins CSS'))
		.pipe(concat('plugins.min.css', { newLine: separator }))
		.pipe(uglifycss())
		.pipe(autoprefixer(autoPreFixerOptions))
		.pipe(gulp.dest(dest.rootPluginsCss));
	done();
}

/*-- Gulp Compile Scss to Css Task & Minify --*/
function styleCss(done) {
	gulp
		.src(src.styleScss)
		.pipe(sourcemaps.init())
		.pipe(customPlumber('Error On Compiling Style Scss'))
		.pipe(sass({ outputStyle: 'expanded' }).on('error', sass.logError))
		.pipe(concat('style.css'))
		.pipe(autoprefixer(autoPreFixerOptions))
		.pipe(sourcemaps.write())
		.pipe(gulp.dest(dest.rootCss))
		.pipe(customPlumber('Error On Compiling & Minifying Style Scss'))
		.pipe(sass({ outputStyle: 'compressed' }).on('error', sass.logError))
		.pipe(concat('style.min.css'))
		.pipe(autoprefixer(autoPreFixerOptions))
		.pipe(sourcemaps.write())
		.pipe(gulp.dest(dest.rootCss));
	done();
}

/*-- Gulp Compile Scss to Css Task & Minify --*/
function scss(done) {
	gulp.src(src.scssAll).pipe(customPlumber('Error On Compiling Style Scss')).pipe(gulp.dest(dest.scss));
	done();
}

/*-- 
    JS Task
--------------------------------------------------------------------*/

/*-- Vendor JS --*/
function vendorJs(done) {
	gulp
		.src(src.rootVendorJs)
		.pipe(customPlumber('Error On Copying Vendor JS'))
		.pipe(gulp.dest(dest.rootVendorJs))
		.pipe(customPlumber('Error On Combining Vendor JS'))
		.pipe(concat('vendor.js', { newLine: separator }))
		.pipe(gulp.dest(dest.rootVendorJs))
		.pipe(customPlumber('Error On Combining & Minifying Vendor JS'))
		.pipe(concat('vendor.min.js', { newLine: separator }))
		.pipe(uglify())
		.pipe(gulp.dest(dest.rootVendorJs));
	done();
}

/*-- All JS Plugins --*/
function pluginsJs(done) {
	gulp
		.src(src.rootPluginsJs)
		.pipe(customPlumber('Error On Copying Vendor JS'))
		.pipe(gulp.dest(dest.rootPluginsJs))
		.pipe(customPlumber('Error On Combining Vendor JS'))
		.pipe(concat('plugins.js', { newLine: separator }))
		.pipe(gulp.dest(dest.rootPluginsJs))
		.pipe(customPlumber('Error On Combining & Minifying Vendor JS'))
		.pipe(concat('plugins.min.js', { newLine: separator }))
		.pipe(uglify())
		.pipe(gulp.dest(dest.rootPluginsJs));
	done();
}

/*-- Copy all JS Plugins Related Folder Form Sourch Plugins Folder To Destination Plugins Folder --*/
function pluginsRelatedJs(done) {
	gulp
		.src(src.pluginsJsRelFolder)
		.pipe(
			customPlumber(
				'Error On Copying all JS Plugins Related Folder Form Sourch Plugins Folder To Destination Plugins Folder'
			)
		)
		.pipe(gulp.dest(dest.rootPluginsJs));
	done();
}

/*-- Copy all JS Plugins Related Folder Form Sourch Root JS Folder To Destination Root JS Folder --*/
function pluginsRelatedJsRoot(done) {
	gulp
		.src(src.pluginsJsRelFolderRoot)
		.pipe(
			customPlumber(
				'Error On Copying all JS Plugins Related Folder Form Sourch Root JS Folder To Destination Root JS Folder'
			)
		)
		.pipe(gulp.dest(dest.rootJs));
	done();
}

/*-- Gulp Main Js Task --*/
function mainJs(done) {
	gulp.src(src.mainJs).pipe(customPlumber('Error On Copying Main Js File')).pipe(gulp.dest(dest.rootJs));
	done();
}

/*-- Gulp Ajax Mail Js Task --*/
function ajaxMail(done) {
	gulp.src(src.ajaxMail).pipe(customPlumber('Error On Copying Ajax Mail Js File')).pipe(gulp.dest(dest.rootJs));
	done();
}

/*-- Gulp PHP Task --*/
function rootPhp(done) {
	gulp.src(src.rootPhp).pipe(customPlumber('Error On Copying Php File')).pipe(gulp.dest(dest.rootPhp));
	done();
}

/*-- 
    Image Optimization
--------------------------------------------------------------------*/
function imageOptimize(done) {
	gulp
		.src(src.images)
		.pipe(imagemin([ imageminUPNG(), mozjpeg(), jpegRecompress(), svgo() ], { verbose: true }))
		.pipe(gulp.dest(dest.images));
	done();
}

/*-- 
    All, Watch & Default Task
--------------------------------------------------------------------*/

/*-- All --*/
gulp.task('clean', cleanProject);
gulp.task(
	'allTask',
	gulp.series(
		fonts,
		html,
		vendorCss,
		pluginsCss,
		styleCss,
		scss,
		vendorJs,
		pluginsJs,
		pluginsRelatedJs,
		pluginsRelatedJsRoot,
		mainJs,
		ajaxMail,
		rootPhp,
		imageOptimize
	)
);

/*-- Watch --*/
function watchFiles() {
	gulp.watch(src.fontsAll, gulp.series(fonts, reload));

	gulp.watch(src.rootHtml, gulp.series(html, reload));
	gulp.watch(src.rootPartials, gulp.series(html, reload));

	gulp.watch(src.rootVendorCss, gulp.series(vendorCss, reload));
	gulp.watch(src.rootPluginsCss, gulp.series(pluginsCss, reload));
	gulp.watch(src.scssAll, gulp.series(styleCss, reload));
	gulp.watch(src.scssAll, gulp.series(scss, reload));

	gulp.watch(src.rootVendorJs, gulp.series(vendorJs, reload));
	gulp.watch(src.rootPluginsJs, gulp.series(pluginsJs, reload));
	gulp.watch(src.pluginsJsRelFolder, gulp.series(pluginsRelatedJs, reload));
	gulp.watch(src.pluginsJsRelFolderRoot, gulp.series(pluginsRelatedJsRoot, reload));
	gulp.watch(src.mainJs, gulp.series(mainJs, reload));
	gulp.watch(src.ajaxMail, gulp.series(ajaxMail, reload));
	gulp.watch(src.rootPhp, gulp.series(rootPhp, reload));

	gulp.watch(src.images, gulp.series(imageOptimize, reload));
}

/*-- Default --*/
gulp.task('default', gulp.series('allTask', gulp.parallel(liveBrowserSync, watchFiles)));
