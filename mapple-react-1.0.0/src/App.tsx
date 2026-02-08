import { Routes, Route } from "react-router-dom";
import { Footer } from "./components/footer";
import LenisScroll from "./components/lenis";
import { Navbar } from "./components/navbar";
import { CtaSection } from "./sections/cta-section";
import { FaqSection } from "./sections/faq-section";
import { FeatureSection } from "./sections/feature-section";
import { HeroSection } from "./sections/hero-section";
import { LogoMarquee } from "./sections/logo-marquee";
import { TestimonialSection } from "./sections/testimonial-section";
import { WorkSection } from "./sections/work-section";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Products from "./pages/Products";

function HomePage() {
    return (
        <>
            <HeroSection />
            <LogoMarquee />
            <FeatureSection />
            <WorkSection />
            <TestimonialSection />
            <FaqSection />
            <CtaSection />
        </>
    );
}

export default function App() {
    return (
        <>
            <Navbar />
            <LenisScroll />
            <main className="mx-4 md:mx-16 lg:mx-24 xl:mx-32 border-x border-gray-800">
                <Routes>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/products" element={<Products />} />
                </Routes>
            </main>
            <Footer />
        </>
    )
}