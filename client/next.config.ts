import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  reactStrictMode: process.env.NODE_ENV === "test" ? false : true,
  async rewrites() {
    return [
      {
        source: "/api/:path*",
        destination: "http://api:8080/:path*",
      },
    ];
  },
};

export default nextConfig;
