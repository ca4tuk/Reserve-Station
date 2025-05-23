cmd-whitelistadd-desc = Добавить игрока в вайтлист сервера.
cmd-whitelistadd-help = Использование: whitelistadd <username или  User ID>
cmd-whitelistadd-existing = { $username } уже находится в вайтлисте!
cmd-whitelistadd-added = { $username } добавлен в вайтлист
cmd-whitelistadd-not-found = Не удалось найти игрока '{ $username }'
cmd-whitelistadd-arg-player = [игрок]
cmd-whitelistremove-desc = Удалить игрока с вайтлиста сервера.
cmd-whitelistremove-help = Использование: whitelistremove <username или  User ID>
cmd-whitelistremove-existing = { $username } не находится в вайтлисте!
cmd-whitelistremove-removed = { $username } удалён с вайтлиста
cmd-whitelistremove-not-found = Не удалось найти игрока '{ $username }'
cmd-whitelistremove-arg-player = [игрок]
cmd-kicknonwhitelisted-desc = Кикнуть всег игроков не в белом списке с сервера.
cmd-kicknonwhitelisted-help = Использование: kicknonwhitelisted
ban-banned-permanent = Этот бан можно только обжаловать. Для этого посетите https://discord.gg/WXZvqzZ2Fc.
ban-banned-permanent-appeal = Этот бан можно только обжаловать. Для этого посетите https://discord.gg/WXZvqzZ2Fc.
ban-expires = Вы получили бан на { $duration } минут, и он истечёт { $time } по UTC (для московского времени добавьте 3 часа).
ban-banned-1 = Вам, или другому пользователю этого компьютера или соединения, запрещено здесь играть.
ban-banned-2 = Администратор, выдавший бан: "{ $adminName }"
ban-banned-3 = Причина бана: "{ $reason }"
ban-banned-4 = Попытки обойти этот запрет, например, создать новый аккаунт, будут регистрироваться.
soft-player-cap-full = Сервер заполнен!
panic-bunker-account-denied = Этот сервер находится в режиме "Бункер", часто используемом в качестве меры предосторожности против рейдов. Новые подключения от аккаунтов, не соответствующих определённым требованиям, временно не принимаются. Повторите попытку позже
whitelist-playtime = У вас недостаточно игрового времени, чтобы присоединиться к этому серверу. Вам нужно не менее { $minutes } минут игрового времени, чтобы присоединиться к этому серверу.
whitelist-player-count = Этот сервер в данный момент не принимает игроков. Пожалуйста, повторите попытку позже.
whitelist-notes = В настоящее время у вас слишком много заметок администратора, чтобы присоединиться к этому серверу. Вы можете проверить свои заметки, набрав в чате команду /adminremarks.
whitelist-manual = Вы не внесены в белый список на этом сервере.
whitelist-blacklisted = Вы занесены в черный список на этом сервере.
whitelist-always-deny = Вам запрещено присоединяться к этому серверу.
whitelist-fail-prefix = Не внесен в белый список: { $msg }
whitelist-misconfigured = Сервер неправильно настроен и не принимает игроков. Пожалуйста, свяжитесь с владельцем сервера и повторите попытку позже.
cmd-blacklistadd-desc = Добавляет игрока с указанным именем пользователя в черный список сервера.
cmd-blacklistadd-help = Использование: blacklistadd <имя пользователя>.
cmd-blacklistadd-existing = { $username } уже находится в черном списке!
cmd-blacklistadd-added = { $username } добавлен в черный список
cmd-blacklistadd-not-found = Невозможно найти '{ $username }'.
cmd-blacklistadd-arg-player = [player]
cmd-blacklistremove-desc = Удаляет игрока с указанным именем пользователя из черного списка сервера.
cmd-blacklistremove-help = Использование: blacklistremove <имя пользователя>
cmd-blacklistremove-existing = { $username } нет в черном списке!
cmd-blacklistremove-removed = { $username } удалено из черного списка
cmd-blacklistremove-not-found = Невозможно найти '{ $username }'.
cmd-blacklistremove-arg-player = [игрок]
panic-bunker-account-denied-reason = Этот сервер находится в режиме "Бункер", часто используемом в качестве меры предосторожности против рейдов. Новые подключения от аккаунтов, не соответствующих определённым требованиям, временно не принимаются. Повторите попытку позже Причина: "{ $reason }"
panic-bunker-account-reason-account = Ваш аккаунт Space Station 14 слишком новый. Он должен быть старше { $minutes } минут
panic-bunker-account-reason-overall =
    Необходимо минимальное отыгранное Вами время на сервере — { $minutes } { $minutes ->
        [one] минута
        [few] минуты
       *[other] минут
    }.
baby-jail-account-denied = Этот сервер - сервер для новичков, предназначенный для новых игроков и тех, кто хочет им помочь. Новые подключения слишком старых или не внесенных в белый список аккаунтов не принимаются. Загляните на другие серверы и посмотрите все, что может предложить Space Station 14. Веселитесь!
baby-jail-account-denied-reason = Этот сервер - сервер для новичков, предназначенный для новых игроков и тех, кто хочет им помочь. Новые подключения слишком старых аккаунтов или аккаунтов, не входящих в белый список, не принимаются. Загляните на другие серверы и посмотрите все, что может предложить Space Station 14. Веселитесь! Причина: «{ $reason }»
baby-jail-account-reason-account = Ваш аккаунт Space Station 14 слишком старый. Он должен быть моложе { $minutes } минут
generic-misconfigured = Сервер неправильно настроен и не принимает игроков. Пожалуйста, свяжитесь с владельцем сервера и повторите попытку позже.
ipintel-server-ratelimited = На этом сервере используется система безопасности с внешней проверкой, которая достигла своего максимального предела проверки. Пожалуйста, обратитесь за помощью к администрации сервера и повторите попытку позже.
ipintel-unknown = На этом сервере используется система безопасности с внешней проверкой, но она столкнулась с ошибкой. Пожалуйста, обратитесь за помощью к администрации сервера и повторите попытку позже.
ipintel-suspicious = Похоже, вы подключаетесь через центр обработки данных или VPN. По административным причинам мы не разрешаем играть через VPN-соединения. Пожалуйста, обратитесь за помощью к администрации сервера, если вы считаете, что это ошибочно.
baby-jail-account-reason-overall =
    Наигранное Вами время на сервере должно быть больше { $minutes } { $minutes ->
        [one] минуты
       *[other] минут
    }.
hwid-required = Ваш клиент отказался отправлять идентификатор оборудования (HWID). Пожалуйста, свяжитесь с администрацией для получения дальнейшей помощи.
