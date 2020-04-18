npm version patch --message "chore: bump and tag version"
MKDIR .params
node -p "require('./package.json').version" > .params/version
SET /p VERSION=<.params/version
git add --all
git commit -m "chore: update app"
git push origin --all
  