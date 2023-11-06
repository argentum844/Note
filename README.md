# Блокнот

На сайте должно быть возможно:
- Зарегистрироваться в личном кабинете
- Зайти в личный кабинет
- Работа с текстовым документом
- Задать вопрос - отобразить FAQ - форма для вопроса - сохранить вопрос и данные пользователя
- Просматривать публичные документы (которые открыты для всех)
- Посмотреть автора документа
- Просмотреть правила сайта

Работа с текстовым документом:
- Создать текстовый документ
- Редактировать текстовый документ
- Открыть и просмотреть (в том числе загрузить с компьютера)
- Удалить
- Сохранить как файл на компьютер

Не зарегистрированный пользователь может работать с документом, но при окончании сессии все его документы будут удалены. Не может создавать публичные документы

Зарегистрированный пользователь может:
- Работать с документом
- Просмотреть список своих документов
- Отображение активности пользователя по времени
- Пользователь может давать другиим пользователям доступ к своему документу, а те при получении доступа могут его редактировать (не удалять)
- Пользователь может сделать документ публичным на просмотр
- Скрыть свое имя как автора документа (подставится "аноним")

На сайте есть админка (внезапно)
Возможности админа:
- Просматривать все документы
- Проверять публичные документы на соответствие правилам сайта
- Удалять не соответствующие правилам публичгые документы
- Выдавать мут (запрет создавать публичные документы / задавать вопросы) пользователям за нарушение правил
- Отвечать на вопросы пользователя
- Забанить пользователя (при многократном нарушении правил)

---

![Mодель](https://github.com/argentum844/Note/assets/58563250/1403f8e5-38f2-4f40-bce7-9d46f91cbad3)
`Список редакторов` - таблица, где пара пользователь-документ означает, что пользователь может редактировать этот документ
Поле `Автор_Отображение` таблицы `Документы` - это то, как будет отображаться имя автора документа при публичном доступе
Поле `Публичность` таблицы `Документы` означает, будет ли документ доступен всем или только создателю на прочтение
В таблице `Вопрос` поле `ID_Ответчика` может быть нулевым, если на вопрос еще не получен ответ
Таблица `Cессия` - это список сеансов пользователей
