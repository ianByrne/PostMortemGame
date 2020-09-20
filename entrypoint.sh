echo "* * * * * root cd /app/ChatScript/BINARIES; ./LinuxChatScript64 > /dev/null
# blankety blank" > /etc/cron.d/start_chatscript_cron

chmod 0644 /etc/cron.d/start_chatscript_cron
crontab /etc/cron.d/start_chatscript_cron
cron

dotnet WebApp.dll