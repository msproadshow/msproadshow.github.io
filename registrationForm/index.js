window.onload = run;
function run() {

    var search = window.location.search.substr(1);
        keys = {};
        search.split('&').forEach(function(item) {
        item = item.split('=');
        keys[item[0]] = item[1];
    });
        var img = document.getElementById('sourceimg');
    switch (keys.city){
        case 'kiev':{
            document.getElementById('kiev').setAttribute('selected', 'selected');
            img.setAttribute('src', '../images/kiyv.png');
            break;
        }
        case 'lviv':{
            document.getElementById('lviv').setAttribute('selected', 'selected');
            img.setAttribute('src', '../images/Lviv.png');
            break;
        }
        case 'dnipro':{
            document.getElementById('dnipro').setAttribute('selected', 'selected');
            img.setAttribute('src', '../images/dnipro.png');
            break;
        }
        case 'vinnytsa':{
            document.getElementById('vinnytsa').setAttribute('selected', 'selected');
            img.setAttribute('src', '../images/Vinnytsia.png');
            break;
        }
        case 'odessa':{
            document.getElementById('odessa').setAttribute('selected', 'selected');
            img.setAttribute('src', '../images/Odesa.png');
            break;
        }
        default:{
            document.getElementById('kiev').setAttribute('selected', 'selected');
            img.setAttribute('src', '../images/logo.png');
            break;
        }
    }



    var work = document.getElementById('work');
    var parent = document.getElementById('other');

    var company = document.createElement('div');
    company.innerHTML = '<div class="form-group" id="companydiv"><label for="company" class="col-sm-3 control-label">Компанія</label>' +
        '<div class="col-sm-9">' +
        ' <input type="text" id="company" placeholder="Компанія" class="form-control" autofocus>' +
        '</div>' +
        '</div>';
    parent.appendChild(company);

    var position = document.createElement('div');
    position.innerHTML = '<div class="form-group" id="positiondiv"><label for="position" class="col-sm-3 control-label">Посада</label>' +
        '<div class="col-sm-9">' +
        ' <input type="text" id="position" placeholder="Посада" class="form-control" autofocus>' +
        '</div>' +
        '</div>';
    parent.appendChild(position);

    work.onclick = function (event) {
        var option = event.target.value;
        if(option=="Працюю"){
            if(document.getElementById('university')) {
                document.getElementById('universitydiv').parentNode.removeChild(document.getElementById('universitydiv'));
                document.getElementById('yeardiv').parentNode.removeChild(document.getElementById('yeardiv'));
                document.getElementById('specialitydiv').parentNode.removeChild(document.getElementById('specialitydiv'));
                document.getElementById('fac').parentNode.removeChild(document.getElementById('fac'));
            }
            if(document.getElementById('companydiv'))
            {
                return;
            }
            var company = document.createElement('div');
            company.innerHTML = '<div class="form-group" id="companydiv"><label for="company" class="col-sm-3 control-label">Компанія</label>' +
                '<div class="col-sm-9">' +
                ' <input type="text" id="company" placeholder="Компанія" class="form-control" autofocus>' +
                '</div>' +
                '</div>';
            parent.appendChild(company);

            var position = document.createElement('div');
            position.innerHTML = '<div class="form-group" id="positiondiv"><label for="position" class="col-sm-3 control-label">Посада</label>' +
                '<div class="col-sm-9">' +
                ' <input type="text" id="position" placeholder="Посада" class="form-control" autofocus>' +
                '</div>' +
                '</div>';
            parent.appendChild(position);
        }
        else{
            if(option=="Навчаюсь"){
                if(document.getElementById('companydiv')) {
                    document.getElementById('companydiv').parentNode.removeChild(document.getElementById('companydiv'));
                    document.getElementById('positiondiv').parentNode.removeChild(document.getElementById('positiondiv'));
                }

                if(document.getElementById('universitydiv')){
                    return;
                }

                var university = document.createElement('div');
                university.innerHTML = '<div class="form-group" id="universitydiv"><label for="university" class="col-sm-3 control-label">Навчальний заклад</label>' +
                    '<div class="col-sm-9">' +
                    ' <input type="text" id="university" placeholder="Навчальний заклад" class="form-control" autofocus >' +
                    '</div>' +
                    '</div>';
                parent.appendChild(university);

                var faculty = document.createElement('div');
                faculty.innerHTML = '<div class="form-group" id="fac"><label for="faculty" class="col-sm-3 control-label">Факультет</label>' +
                    '<div class="col-sm-9">' +
                    ' <input type="text" id="faculty" placeholder="Факультет" class="form-control" autofocus>' +
                    '</div>' +
                    '</div>';
                parent.appendChild(faculty);

                var year = document.createElement('div');
                year.innerHTML = '<div class="form-group" id="yeardiv"><label for="year" class="col-sm-3 control-label">Курс</label>' +
                    '<div class="col-sm-9">' +
                    ' <input type="text" id="year" placeholder="Курс" class="form-control" autofocus>' +
                    '</div>' +
                    '</div>';
                parent.appendChild(year);

                var speciality = document.createElement('div');
                speciality.innerHTML = '<div class="form-group" id="specialitydiv"><label for="speciality" class="col-sm-3 control-label">Спеціальність</label>' +
                    '<div class="col-sm-9">' +
                    ' <input type="text" id="speciality" placeholder="Спеціальність" class="form-control" autofocus>' +
                    '</div>' +
                    '</div>';
                parent.appendChild(speciality);
        }
    };

};
    function handleSubmit(event) {
        event.preventDefault();
        console.log('register');
    };

    var registerButton = document.getElementById("registerButton");
  /*  registerButton.addEventListener('click',handleSubmit);*/
}