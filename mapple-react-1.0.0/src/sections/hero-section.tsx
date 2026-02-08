import { ArrowDownRightIcon, StarIcon, TrendingUpIcon } from "lucide-react";

export const HeroSection = () => {
    return (
        <>
            <div className="flex flex-col max-md:px-2 items-center justify-center">
                <div className="mt-32 flex items-center justify-center gap-2">
                    <TrendingUpIcon className="size-5" />
                    Trusted by 50,000+ users worldwide
                </div>
                <h1 className="text-center font-urbanist text-[42px]/13 md:text-6xl/20 font-bold max-w-2xl bg-linear-to-r from-white to-white/50 bg-clip-text text-transparent">
                    Everything you need to <span className="text-primary">scale</span> your agency
                </h1>
                <p className="text-center text-base text-zinc-300 max-w-lg mt-4">
                    Explore amet minim mollit non deserunt ullamco est sit aliqua dolor do amet sint. Velit officia consequat.
                </p>
                <div className="mt-8 flex items-center justify-center gap-4">
                    <button className="bg-primary hover:bg-secondary transition duration-300 text-black px-6 py-2.5 rounded-lg">
                        Get Started
                    </button>
                    <button className="border border-gray-600 text-zinc-300 px-4 py-2.5 rounded-lg hover:bg-gray-900">
                        See How It Works
                        <ArrowDownRightIcon className="ml-1 size-5 inline-flex" />
                    </button>
                </div>
                <div className="mt-10 flex items-center justify-center gap-2">
                    <img
                        src="/rating-logo-mark.svg"
                        alt="ration logo"
                        width={100}
                        height={100}
                        className="size-5"
                    />
                    <div className="flex items-center">
                        {...Array(5).fill(0).map((_, i) => (
                            <StarIcon key={i} className="size-4 fill-yellow-500 text-yellow-500" />
                        ))}
                    </div>
                    <div className="h-5 w-px bg-gray-400" />
                    <p className="text-gray-400 line-clamp-1">
                        Innovating UI solution 2025 by Prebuiltui
                    </p>
                </div>
            </div>
            <div className="p-3 md:p-6 w-full mt-16 border-t border-gray-800">
                <img
                    src='/hero-section-image.png'
                    height={500}
                    width={1440}
                    alt="Hero Section Image"
                    className="w-full h-auto rounded-2xl"
                />
            </div>
        </>
    );
};