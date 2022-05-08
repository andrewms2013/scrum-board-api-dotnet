PH=$(echo 'password' | docker run --rm -i datalust/seq config hash)

mkdir -p ~/.seq

docker run \
  --name seq \
  -d \
  --restart unless-stopped \
  -e ACCEPT_EULA=Y \
  -e SEQ_FIRSTRUN_ADMINPASSWORDHASH="$PH" \
  -v ~/.seq:/data \
  -p 5341:80 \
  datalust/seq

# Default admin and password are "admin" and "password"
