Asp-net Core MVC test project

#### Multi Arch Image
```bash
docker build -t emgi2/guran_2_2023:latest-amd64 .
docker build -t emgi2/guran_2_2023:latest-arm64 .
docker push emgi2/guran_2_2023:latest-amd64
docker push emgi2/guran_2_2023:latest-arm64
```
Windows CMD
```bash
docker manifest create emgi2/guran_2_2023:latest ^
    emgi2/guran_2_2023:latest-amd64 ^
    emgi2/guran_2_2023:latest-arm64 ^
    --amend
```
```bash
docker manifest push --purge emgi2/guran_2_2023:latest
```

#### Linux
```bash
docker manifest create emgi2/guran_2_2023:latest \
    emgi2/guran_2_2023:latest-amd64 \
    emgi2/guran_2_2023:latest-arm64 \
    --amend
```

```bash
docker manifest push --purge emgi2/guran_2_2023:latest
```