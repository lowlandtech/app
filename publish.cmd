git add --all
git commit -m "chore: update app"
npm version patch --clean --message "chore: bump and tag version"
MKDIR .params
node -p "require('./package.json').version" > .params/version
SET /p VERSION=<.params/version
git push origin --all
  