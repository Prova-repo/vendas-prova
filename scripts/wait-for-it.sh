host="$1"
shift
port="$1"
shift

timeout="${WAIT_TIMEOUT:-15}"
echo "Aguardando $host:$port por $timeout segundos..."
while ! timeout 1 bash -c "cat < /dev/null > /dev/tcp/$host/$port"; do
  timeout=$((timeout-1))
  if [ "$timeout" -le 0 ]; then
    echo "Tempo esgotado aguardando conexão com $host:$port."
    exit 1
  fi
  sleep 1
done
echo "$host:$port está pronto!"
exec "$@"