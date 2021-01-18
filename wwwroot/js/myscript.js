document.addEventListener("DOMContentLoaded", function (event) {
    function createElement(name, value = "") {
        var myelm = document.createElement("div");
        myelm.id = name;
        myelm.className = name;
        myelm.innerHTML = value;
        var s = document.getElementsByTagName('script')[0];
        if (s) {
            s.parentNode.insertBefore(myelm, s);
        } else {
            document.body.appendChild(myelm);
        }
    }

    function destroyElement(name) {
        if (document.getElementById(name)) {
            document.getElementById(name).remove();
        }
    }

    function loadSound(load = false) {
        if (load == true) {
            function listSounds() {
                var ary = [
                    {
                        id: 1,
                        title: 'Radio M80',
                        src: 'https://mcrscast.mcr.iol.pt/m80',
                        type_obj: 'radio'
                    },
                    {
                        id: 2,
                        title: 'Radio Comercial',
                        src: 'https://mcrscast1.mcr.iol.pt/comercial.mp3',
                        type_obj: 'radio'
                    },
                    {
                        id: 3,
                        title: 'Radio Nova Era',
                        src: 'https://centova.radios.pt/proxy/478?mp=/stream',
                        type_obj: 'radio'
                    }
                ];

                ary = ary.filter(x => ['radio'].includes(x.type_obj));
                return ary;
            }

            var ary_sounds = listSounds();
            var myary = [];
            var isSongPaused = false;

            if (document.getElementById("radiobar")) {
                document.getElementById("radiobar").innerHTML = "" +
                "<div class='row align-items-center justify-content-center'>\
                    <div class='col-12 col-md-6 col-lg-4 colradioblkleft'>\
                        <span class='infomusic mr-3' id='infomusic'>Loading...</span>\
                    </div>\
                    <div class='col-12 col-md-6 col-lg-4 colradioblkcenter'>\
                        <div class='d-flex flex-row align-items-center justify-content-center'>\
                            <select class='listsounds w-auto custom-select' id='listsounds'></select>\
                        </div>\
                    </div>\
                    <div class='col-12 col-md-6 col-lg-4 colradioblkright'>\
                        <input type='range' id='volmusic' class='volmusic w-auto mr-2' min='0' max='1' step='0.01' value='0.5' />\
                        <button class='btn btn-primary btnprevsong' id='btnprevsong'>Prev</button>\
                        <button class='btn btn-primary btnnextsong' id='btnnextsong'>Next</button>\
                        <button class='btn btn-primary btnplaypausesong' id='btnplaypausesong'>Play/Pause</button>\
                        <button class='btn btn-primary btnstopsong' id='btnstopsong'>Stop</button>\
                        <button class='btn btn-primary btnunloadradio' id='btnunloadradio'>Unload</button>\
                    </div>\
                    <div class='clearfix'></div>\
                </div>";

                if (ary_sounds.length > 0) {
                    for (var i = 0; i < ary_sounds.length; i++) {
                        document.getElementById('listsounds').innerHTML += "\
                             <option class='listsoundsopt"+ i + "' id='listsoundsopt" + i + "' value='" + ary_sounds[i].src + "'>"
                            + ary_sounds[i].title +
                            "</option>";

                        myary.push(ary_sounds[i].src);
                    }
                }

                var volmusic = (document.getElementById('volmusic')) ? document.getElementById('volmusic').value : 0.5;
                var indlist = (document.getElementById('listsounds')) ? document.getElementById('listsounds').selectedIndex : 0;

                var sound = new Howl({
                    src: myary,
                    html5: true,
                    format: ['mp3', 'aac'],
                    autoplay: false,
                    loop: false,
                    preload: true,
                    volume: volmusic,
                    xhr: {
                        method: 'GET',
                        headers: {
                            'Access-Control-Allow-Origin': '*'
                        },
                        withCredentials: false
                    },
                    onload: function () {
                        if (document.getElementById('infomusic')) {
                            document.getElementById('infomusic').innerHTML = 'The music player has loaded ' + ary_sounds[indlist].title;
                        }
                    },
                    onplay: function () {
                        if (document.getElementById('infomusic')) {
                            document.getElementById('infomusic').innerHTML = 'The music player is now playing ' + ary_sounds[indlist].title;
                        }
                    },
                    onpause: function () {
                        if (document.getElementById('infomusic')) {
                            document.getElementById('infomusic').innerHTML = 'The music player is now paused';
                        }
                    },
                    onstop: function () {
                        if (document.getElementById('infomusic')) {
                            document.getElementById('infomusic').innerHTML = 'The music player is now stopped';
                        }
                    },
                    onloaderror: function (err) {
                        console.log('Unable to load song. Details: ' + err)
                    },
                    onplayerror: function () {
                        sound.once('unlock', function () {
                            sound.play();
                        });
                    },
                    onend: function () {
                        console.log('finished');
                    }
                });

                if (sound.autoplay == true) {
                    sound.play();
                }

                if (document.getElementById('listsounds')) {
                    document.getElementById('listsounds').addEventListener("change", function () {
                        indlist = document.getElementById('listsounds').selectedIndex;

                        if (document.querySelectorAll('#listsounds option').length > 0) {
                            for (var lopx = 0; lopx < document.querySelectorAll('#listsounds option').length; lopx++) {
                                document.querySelectorAll('#listsounds option')[lopx].removeAttribute('selected');
                            }

                            if (document.querySelectorAll('#listsounds option')[indlist]) {
                                document.querySelectorAll('#listsounds option')[indlist].setAttribute('selected', 'selected');
                            }
                        }

                        isSongPaused = false;

                        sound.stop();
                        sound.unload();
                        sound._src = myary[indlist];
                        sound.load();

                        if (sound.autoplay == true) {
                            sound.play();
                        }
                    });
                }

                if (document.getElementById('volmusic')) {
                    document.getElementById('volmusic').addEventListener("change", function () {
                        sound.volume(this.value);
                    });
                }

                if (document.getElementById('btnprevsong')) {
                    document.getElementById('btnprevsong').addEventListener("click", function () {
                        if (indlist > 0 && indlist <= (ary_sounds.length - 1)) {
                            indlist--;
                        } else {
                            indlist = ary_sounds.length - 1;
                        }

                        if (document.querySelectorAll('#listsounds option').length > 0) {
                            for (var lopx = 0; lopx < document.querySelectorAll('#listsounds option').length; lopx++) {
                                document.querySelectorAll('#listsounds option')[lopx].removeAttribute('selected');
                            }

                            if (document.querySelectorAll('#listsounds option')[indlist]) {
                                document.querySelectorAll('#listsounds option')[indlist].setAttribute('selected', 'selected');
                            }
                        }

                        isSongPaused = false;

                        sound.stop();
                        sound.unload();
                        sound._src = myary[indlist];
                        sound.load();

                        if (sound.autoplay == true) {
                            sound.play();
                        }
                    });
                }

                if (document.getElementById('btnnextsong')) {
                    document.getElementById('btnnextsong').addEventListener("click", function () {
                        if (indlist >= 0 && indlist < (ary_sounds.length - 1)) {
                            indlist++;
                        } else {
                            indlist = 0;
                        }

                        if (document.querySelectorAll('#listsounds option').length > 0) {
                            for (var lopx = 0; lopx < document.querySelectorAll('#listsounds option').length; lopx++) {
                                document.querySelectorAll('#listsounds option')[lopx].removeAttribute('selected');
                            }

                            if (document.querySelectorAll('#listsounds option')[indlist]) {
                                document.querySelectorAll('#listsounds option')[indlist].setAttribute('selected', 'selected');
                            }
                        }

                        isSongPaused = false;

                        sound.stop();
                        sound.unload();
                        sound._src = myary[indlist];
                        sound.load();

                        if (sound.autoplay == true) {
                            sound.play();
                        }
                    });
                }

                if (document.getElementById('btnplaypausesong')) {
                    document.getElementById('btnplaypausesong').addEventListener("click", function () {
                        if (isSongPaused == false) {
                            isSongPaused = true;
                            sound.play();
                        } else {
                            isSongPaused = false;
                            sound.pause();
                        }
                    });
                }

                if (document.getElementById('btnstopsong')) {
                    document.getElementById('btnstopsong').addEventListener("click", function () {
                        sound.stop();
                        isSongPaused = false;
                    });
                }

                if (document.getElementById('btnunloadradio')) {
                    document.getElementById('btnunloadradio').addEventListener("click", function () {
                        sound.unload();
                        loadRadioBar(true);
                    });
                }
            }
        }
    }

    function loadRadioBar(load = false) {
        if (load == true) {
            destroyElement("radiobar");
            createElement("radiobar", "<button class='btn btn-primary btnloadradio' id='btnloadradio'>Load</button>");

            if (!document.body.classList.contains('radiobarfixed')) {
                document.body.classList.add('radiobarfixed');
            }

            if (document.getElementById("btnloadradio")) {
                document.getElementById("btnloadradio").addEventListener("click", function () {
                    loadSound(true);
                });
            }
        }
    }

    loadRadioBar(true);
});