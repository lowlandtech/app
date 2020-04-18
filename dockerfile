### STAGE 1: Build ###

# We label our stage as 'builder'
FROM node:12 as builder
#FROM node:9-alpine as builder

COPY package.json package-lock.json ./

RUN npm set progress=false && npm config set depth 0 && npm cache clean --force

## Storing node modules on a separate layer will prevent unnecessary npm installs at each build
RUN npm install --only=prod && mkdir /ng-app && cp -R ./node_modules ./ng-app

WORKDIR /ng-app

COPY . .

## Build the angular app in production mode and store the artifacts in dist folder
RUN $(npm bin)/ng build --prod

## From 'builder' stage copy over the artifacts in dist folder to default nginx public folder
COPY /ng-app/dist/apps/lowlandtech  /usr/share/nginx/html

EXPOSE 4200

#HEALTHCHECK --interval=5m --timeout=3s CMD curl --fail localhost:4200 -O /dev/null || exit 1
CMD $(npm bin)/ng serve --host 0.0.0.0
