FROM owasp/dependency-track

USER root
RUN apk update && apk add curl
USER dtrack
ENV JAVA_OPTS="-Xmx16G"

