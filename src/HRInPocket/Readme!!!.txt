﻿Для запуска проекта требуется провести некоторые телодвижения:

1) В первую очередь - надо ОБЯЗАТЕЛЬНО удалить старую базу данных и все ее миграции (всю папку с миграциями).
2) Заново провести миграции базы данных и сделать ее апдейт (не дошли руки написать нормальный инициализатор и сидер)
3) Запускать нужно 2 проекта одновременно. При чем первым должен стартовать IdentityServer, а клиент (HRInPocket) - последним.
   В середину можно всталять запуск каких-нибудь других API если они будут
4) Чтобы наш сайт смог подключаться к серверу аутентификации, а тот в свою очередь к другим серверам (соцсети) - требуются секретные ключи.
   Файлы (их 2) с секретными ключами я выложу в группу проекта. Нужно их содержимое скопировать к секретам на вашем ПК, и все будет работать.

   ПКМ на проекте HRInPocket => Управление секретами пользователей => откроется обычный json, в который нужно скопировать содержимое файла HRInPocket.json (скину в группу)
   Для проекта Services/HRInPocket.IdentityServer - никакие манипуляции не нужны. Обе базы данных создаются и наполняются при старте. Нужно только добавить секреты... 