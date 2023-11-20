Asp-net Core MVC test project

#### Multi Arch
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