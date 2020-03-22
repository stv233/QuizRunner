<h1>Техническое задание к курсовому проекту по ТРПО</h1>
<p>В своём проекте мы реализуем программу системы проведения тестирования (QuizRunner). Данная программа будет создавать
    набор тестовых заданий, проводить тестирование и анализировать результаты.
    Программа разрабатывается для операционных систем: Linux Ubuntu, macOS, Windows.</p>
<h2>В проекте будет реализовано три режима работы:</h2>
<ol>
    <li>
        <b>Редактор тестов.</b> В этом режиме будут реализовано создание тестовых заданий. Пользователю будет предложено
        ввести количество вопросов. Далее по порядку пользователю надо будет вводить вопросы и варианты ответов,
        отметить правильный ответ, ввести цену вопроса.
    </li>
    <li>
        <b>Тестовая оболочка.</b> В этом режиме будет проводиться само тестирование. Пользователю предлагается загрузить
        определенный тест. После его начала на экране выводится вопрос и варианты ответов, седи которых необходимо
        выбрать правильный.
    </li>
    <li>
        <b>Результаты тестирования.</b> В этом режиме будут проводиться анализ и просмотр результатов тестирования.
        Пользователь сможет увидеть количество правильных ответов и процентное соотношение, а также свой рейтинг среди
        всех проходивших этот тест.
    </li>
</ol>
<h2>Формат входных данных:</h2>
<p>На вход программе подается текстовый файл, содержащий текст.</p>
<h2>Интерфейс приложения:</h2>
<p>Приложение работает в интерактивном режиме, при запуске программы открывается окно, в котором отрисовывается
    стартовый экран, который включает в себя кнопки, отвечающие за основной функционал программы (создание теста,
    загрузка/открытие теста, просмотр статистики), при нажатии на которые отрисовывается экран, с соответственным
    функционалом.</p>
<p>Например, при нажатии на кнопку создания, отрисуется экран, на котором располагаются кнопки и поля ввода текста,
    предназначенные для ввода названия теста, самих вопросов, правильных ответов, и кол-ва баллов, начисляемых за ответ.
</p>
<p>При нажатии на кнопку загрузки, программа отрисует экран, отвечающий за выбор из перечисленных вариантов теста, с
    последующей отрисовкой самого теста и началом его прохождения путём выбора ответа на вопрос и переходу к следующему,
    при нажатии на кнопку подтверждения, в конце тестирования пользователь сможет просмотреть свои результаты
    прохождения теста.</p>
<p>При нажатии на кнопку просмотра статистики, программа отрисует экран, отображающий общую статистику по пройдёным
    тестам.</p>
