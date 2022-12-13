FROM node:latest as build
RUN mkdir -p /app
WORKDIR /app
COPY package*.json /app/
RUN npm install --force
COPY . /app/
RUN npm run build

FROM nginx:latest
COPY /nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/weather-app-front/ /usr/share/nginx/html
EXPOSE 4200
